using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Database.Contracts;
using Database.Models;
using Database.OModels;
using Repository.Contracts;
using Repository.Implimentation;
using static Database.AllEnum;

namespace Repository
{
    public class InterviewReminderMailRepository
    {
        private readonly WorkerSettings _configuration;
        private readonly ITechnicalDAL _technicalDAL;
        private readonly ICommunicationRepository _communicationRepository;

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public InterviewReminderMailRepository(ITechnicalDAL technicalDAL,
            IOptions<WorkerSettings> configuration, ICommunicationRepository communicationRepository
            )
        {
            _technicalDAL = technicalDAL;
            _configuration = configuration.Value;
            _communicationRepository = communicationRepository;
        }

        public void TriggerEmailAll()
        {


            // Get all interviews schedule for today
            List<InterviewSchedule> interviewSchedule = _technicalDAL.GetComing90InterviewSchedules();

            // Get the current time in the Indian time zone
            DateTime cur_time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            // Define constants for time intervals
            TimeSpan ninetyMinutes = TimeSpan.FromMinutes(90);
            TimeSpan eightyMinutes = TimeSpan.FromMinutes(80);
            // Directory path for log files
            var dirpath = System.IO.Path.Combine(_configuration.Path, "email_log");
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            //delete files which are created 1 hour ago
            DeleteOldFiles(dirpath);
            string logPath = System.IO.Path.Combine(dirpath, cur_time.ToString("ddMMMyyHHmm") + ".txt");



            using (StreamWriter writer = new StreamWriter(logPath))
            {
                writer.WriteLine("Interview Scheduled Found: " + interviewSchedule.Count());
                // Iterate through each interview schedule
                foreach (InterviewSchedule interview in interviewSchedule)
                {
                   
                    writer.WriteLine("Interview Scheduled On: " + interview.ScheduledOn.ToString());
                    if (interview.ScheduledOn > cur_time && (interview.ScheduledOn - cur_time > eightyMinutes && interview.ScheduledOn - cur_time <= ninetyMinutes))
                    {
                        writer.WriteLine("Starting for : " + interview.CandidateId.ToString());
                        // Check if mail already send on that date
                        bool isresentcand = _technicalDAL.IsReminderSentCand(interview.Id);
                        bool isresentexpert= _technicalDAL.IsReminderSentExpert(interview.Id);

                        //Send email to Candidate
                        var candidatemail = SendEmailAndWhatsApptoCandidateAll(interview, true);
                        writer.WriteLine(candidatemail);

                        //Send email to Expert
                        var expertmail = SendEmailAndWhatsApptoExpertAll(interview, true);
                        writer.WriteLine(expertmail);
                    }
                }
            }
        }

        private void DeleteOldFiles(string folderPath)
        {
            //Retrive all files from the folder
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                DateTime creationTime = fileInfo.CreationTime;
                DateTime oneDayAgo = DateTime.Now.AddDays(-1);
                // Delete all files which are created before 1 day
                if (creationTime < oneDayAgo)
                {
                    System.IO.File.Delete(file);
                }
            }
        }
        private string SendEmailAndWhatsApptoCandidateAll(InterviewSchedule interview, bool isReminder = false)
        {
            bool isreEmailsentcand = _technicalDAL.IsReminderSentCand(interview.Id);
            bool isreMsgsentcand = _technicalDAL.IsReminderMsgSentCand(interview.Id);
            if (!isreEmailsentcand || !isreMsgsentcand)
            {
                // Get candidate details
                Candidate candidate = _technicalDAL.GetCandidateDetails(interview.CandidateId);
                // Get job details
                JobReqModel jobRequirement = _technicalDAL.GetJobDetails(interview.JobId);
                // Get customer details
                // Customer cust = _technicalDAL.GetCustomerDetails(jobRequirement.CustomerId);
                // Get spoc details
                Spoc spoc = _technicalDAL.GetSpocDetails((int)interview.VproPleSpoc);
                // Check if interview is rescheduled or not
                bool isReschedule = _technicalDAL.IsReschedule(interview.Id);

                //email template path
                string path = System.IO.Path.Combine(_configuration.Path, "email_template/candidate/interview_invite.html");
                string text = System.IO.File.ReadAllText(path);
                string imagetext = "";
                System.Net.Mail.Attachment logo = null;
                //customer logo path
                string logopath = "";
                try
                {
                    if (jobRequirement.CustomerImg != null)
                    {
                        logopath = System.IO.Path.Combine(_configuration.Path, "customer_image/", jobRequirement.CustomerImg);
                        logo = new System.Net.Mail.Attachment(logopath);
                        imagetext = String.Format(@"<img width=""200px"" src=""cid:{0}"" />", logo.ContentId);
                    }
                }
                catch (Exception ex)
                {

                }
                //create meeting link
                string plat_link = _configuration.AbsolutePath + "interview-platform-v1/" + interview.UniqueCode + "/intro";
                if (interview.UniqueCode == null)
                    plat_link = interview.Meetinglink;

                //create scheduled time in candidate's time zone
                DateTime sch_time = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), candidate.TimeZone);
                string scheduletime = sch_time.ToString("dd MMM yyyy hh:mm tt") + " (" + Utility.ShortTimeZone(candidate.TimeZone) + ")";

                // subject for the mail
                string subject = jobRequirement.CustomerName + " Interview Invite: " + jobRequirement.Position + " -" + scheduletime + "-" + candidate.Name;
                List<string> hremails = new List<string>();

                string reptext = "";

                //check if candidate has confirmed or not, if not generate confirmation link in mail
                string confirmlinkCandidate = "";
                if (interview.IsCandidateConfirmByS == false && interview.IsCandidateConfirm == false)
                {
                    var res = generateConfirmationCode(interview);
                    confirmlinkCandidate = _configuration.AbsolutePath + "candidate-confirmation/" + res[0];
                    reptext = res[1];
                }

                //replace all text in the template
                text = text.Replace("$candidatename$", candidate.Name)
                    .Replace("$company$", jobRequirement.CustomerName).Replace("$meetinglink$", plat_link)
                   .Replace("$time$", scheduletime)
                   .Replace("$position$", jobRequirement.Position)
                   .Replace("$jdtext$", jobRequirement.Description).Replace("$title$", subject)
                   .Replace("$ImageText$", imagetext).Replace("$CustomerName$", jobRequirement.CustomerName)
                   .Replace("$spoc$", spoc.Name + " (" + spoc.Phone.Trim() + ")")
                   .Replace("$difftime$", reptext)
                   .Replace("$ConfirmLink$", confirmlinkCandidate);

                if (isReschedule)
                {
                    text = text.Replace("$Note$", "Please note that no further change in schedule would be allowed.");
                }
                else
                {
                    text = text.Replace("$Note$", " ");
                }

                System.Net.Mail.Attachment attachment = null;

                if (isReminder)
                {
                    subject = "Gentle Reminder: " + subject;
                }
                else
                {
                    hremails = _technicalDAL.GetCandidateOwnerEmails(candidate.CandidateId, jobRequirement.JobId);
                }

                CustomerEmailConfig emailConfig = this.GetEmailConfig(jobRequirement.CustomerId);
                var result = "";
                var resc = "";
                if (!isreEmailsentcand)
                {
                    //send email to candidate
                    result = _communicationRepository.CandidateEmailAll(subject, text, candidate.Email, logo, logopath, emailConfig, attachment, hremails);
                    if (result == "Reminder Email Sent Successfully to Candidate " + candidate.Email)
                    {
                        //Add external note
                        var note = "Reminder Email Sent to Candidate";
                        //var updateNotecand = _technicalDAL.UpdateScheduleNotes(interview.Id, note, "VEmp101", true);

                        var schnote = new ScheduleNote
                        {
                            SchId = interview.Id,
                            CreatedBy = "VEmp101",
                            CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                            Notes = note,
                            IsExternal = true
                        };
                        _technicalDAL.AddScheduleNotes(schnote);
                    }
                }
                string[] paramc = { candidate.Name.Trim(), jobRequirement.Position, scheduletime, jobRequirement.CustomerName };
                string phonenoc = "+" + candidate.CountryCode.Trim().Replace("+", "") + candidate.Phone.Trim();
                if (!isreMsgsentcand)
                {
                    // send whatsapp message to candidate
                    resc = _communicationRepository.WhatsAppSendMsgAPI(phonenoc, "VEmp101", WhatsAppCampaign.CandidateInterviewReminder, paramc, _configuration.WhatsAppOpsUser, interview.Id);
                    if (resc.Contains("Message Sent Successfully"))
                    {
                        var wanote = "Reminder Message Sent to Candidate";
                        var schnote = new ScheduleNote
                        {
                            SchId = interview.Id,
                            CreatedBy = "VEmp101",
                            CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                            Notes = wanote,
                            IsExternal = true
                        };
                        _technicalDAL.AddScheduleNotes(schnote);
                    }
                }
                result += Environment.NewLine + resc;
                return result;
            }
            else 
            return "Reminder Email and Msg already sent to candidate.";
        }
        public List<string> generateConfirmationCode(InterviewSchedule interview)
        {
            if (interview.UniqueCode == null)
            {
                // Generate unique code
                string uniqcode = Utility.GenerateUniqueCode(9);
                var chk = _technicalDAL.CheckConfirmUniqueCode(uniqcode);
                while (chk == true)
                {
                    chk = _technicalDAL.CheckConfirmUniqueCode(uniqcode);
                    if (chk == true)
                    {
                        uniqcode = Utility.GenerateUniqueCode(9);
                    }

                }
                _technicalDAL.AddUniqCode(interview.Id, uniqcode);
                interview.UniqueCode = uniqcode;
            }
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string time = "Kindly click the \"Confirm\" button below to confirm your attendance for this interview ASAP <br/> <a target=\"_blank\" style=\"padding: 5px 20px; display: inline-block; text-decoration: none; border-radius: 5px; background: #ff8b00; margin-left: 10px; color: #ffffff; \" href=\"$ConfirmLink$\" >Confirm</a> ";
            List<string> val = new List<string>();
            val.Add(interview.UniqueCode);
            val.Add(time);
            return val;
        }

        public CustomerEmailConfig GetEmailConfig(int cid)
        {
            var data = _technicalDAL.GetEmailConfig(cid);
            if (data != null)
            {
                data.Password = Utility.base64Decode(data.Password);
                return data;
            }
            else
            {
                return new CustomerEmailConfig()
                {
                    EmailType = "Google",
                    EnableSsl = true,
                    FromEmail = _configuration.FromEmail,
                    HostName = _configuration.HostName,
                    PortNo = 587,
                    Password = _configuration.EmailPassword
                };
            }
        }
        public string SendEmailAndWhatsApptoExpertAll(InterviewSchedule interview, bool isReminder = false)
        {
            bool isreEmailsentexp = _technicalDAL.IsReminderSentExpert(interview.Id);
            bool isreMsgsentexp = _technicalDAL.IsReminderMsgSentExpert(interview.Id);
            if (!isreEmailsentexp || !isreMsgsentexp)
            {
                // Get expert details
                Expert exp = _technicalDAL.GetExpertId(interview.ExpertId);
                // get candidate details
                Candidate candidate = _technicalDAL.GetCandidateDetails(interview.CandidateId);
                // get job details
                JobReqModel jobRequirement = _technicalDAL.GetJobDetails(interview.JobId);
                // Get candidate feedback
                //CandidateFeedback candidateFeedback = _technicalDAL.GetCandidateFeedbackBySchedule(interview.Id);

                //get expert scheduled time in ist
                string expertscheduletime = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), exp.TimeZone).ToString("dd MMM yyyy hh:mm tt");
                string tz = Utility.ShortTimeZone(exp.TimeZone);
                var datetz = expertscheduletime + " (" + tz + ")";
                //email template path
                var path = System.IO.Path.Combine(_configuration.Path, "email_template/expert/interview_feedback.html");
                string text = System.IO.File.ReadAllText(path);


                string reptext = "";
                string confirmlinkExpert = "";
                string result = "";
                string WaMsgexp = "";
                //check if expert has confirmed or not, if not generate confirmation link in mail
                if (interview.IsExpertConfirmByS == false && interview.IsExpertConfirm == false)
                {
                    var res = generateConfirmationCode(interview);
                    confirmlinkExpert = _configuration.AbsolutePath + "expert-confirmation/" + Utility.EncodeValue(interview.ExpertId) + "-" + res[0];
                    reptext = res[1];
                }
                //replace all text in the mail
                text = text.Replace("$ExpertName$", exp.Name)
                    .Replace("$company$", jobRequirement.CustomerName)
                    .Replace("$time$", datetz)
                    .Replace("$Position$", jobRequirement.Position)
                    .Replace("$difftime$", reptext)
                    .Replace("$ConfirmLink$", confirmlinkExpert);

                // mail subject
                string subject = "Reminder - VProPle Expert Invite: " + jobRequirement.Position + "-" + datetz + "-" + candidate.Name;
                if (exp.Id != "VExp100")
                {
                    VPEmailConfig emailConfig = this.GetVPEmailConfig();
                    if (!isreEmailsentexp)
                    {
                        result = _communicationRepository.EmailAllExpert(subject, text, exp.EmailId, emailConfig, _configuration.Path, null);
                        if (result == "Reminder Email Sent to Expert " + exp.EmailId)
                        {
                            //Add internal note]
                            var note = "Reminder Email Sent to Expert " + "(" + exp.Name + ")";


                            var schnotedtls = new ScheduleNote
                            {
                                SchId = interview.Id,
                                CreatedBy = "VEmp101",
                                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                                Notes = note,
                                IsExternal = false
                            };
                            _technicalDAL.AddScheduleNotes(schnotedtls);
                        }
                    }
                    string[] param = { exp.Name.Trim(), datetz };
                    string phoneno = "+" + exp.CountryCode.Trim().Replace("+", "") + exp.WhatsAppNo.Trim();
                    if (!isreMsgsentexp)
                    {
                        WaMsgexp = _communicationRepository.WhatsAppSendMsgAPI(phoneno, "VEmp101", WhatsAppCampaign.ExpertInterviewReminder, param, _configuration.WhatsAppERMUser, interview.Id);
                        if (WaMsgexp.Contains("Message Sent Successfully"))
                        {
                            var wanote = "Reminder Message Sent to Expert " + "(" + exp.Name + ")";
                            var schnote = new ScheduleNote
                            {
                                SchId = interview.Id,
                                CreatedBy = "VEmp101",
                                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                                Notes = wanote,
                                IsExternal = false
                            };
                            _technicalDAL.AddScheduleNotes(schnote);
                        }
                    }
                    result += Environment.NewLine + WaMsgexp;
                    return result;
                }
                else
                {
                    return "Reminder Email not sent to Vprople Expert";
                }
            }
            else
                return "Reminder Email and Whatsapp Msg already sent to expert";

        }
        public VPEmailConfig GetVPEmailConfig()
        {
            return new VPEmailConfig()
            {
                FromEmail = _configuration.FromEmail,
                HostName = _configuration.HostName,
                EmailPassword = _configuration.EmailPassword
            };
        }
    }
}
