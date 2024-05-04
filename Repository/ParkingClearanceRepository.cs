using System;
using System.Collections.Generic;
using Database.Models;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Database.Implementation;
using Database.OModels;
using Database;
using static Database.AllEnum;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Database.Contracts;
using System.Text;
using Microsoft.Extensions.Logging;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Newtonsoft.Json;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Repository.Contracts;
using Repository.Implimentation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using System.Threading;
using Database.Models;
using Microsoft.IdentityModel.Tokens;
using Repository;



namespace Repository
{
    public class ParkingClearanceRepository
    {
        private readonly WorkerSettings _configuration;
        private readonly ITechnicalDAL _technicalDAL;
        private readonly ICommunicationRepository _communicationRepository;

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public ParkingClearanceRepository(ITechnicalDAL technicalDAL,
            IOptions<WorkerSettings> configuration, ICommunicationRepository communicationRepository
            )
        {
            _technicalDAL = technicalDAL;
            _configuration = configuration.Value;
            _communicationRepository = communicationRepository;
        }

        public void ClearParking()
        {

            //Auto Schedule Code
            if (_technicalDAL.GetAuthSchedulerSetting() == "ON")
            {
                // Directory path for log files
                var dirpath = System.IO.Path.Combine(_configuration.Path, "schedule_info");
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }

                // Define time range for processing
                DateTime startTime = DateTime.Today.AddHours(9);
                DateTime endTime = DateTime.Today.AddHours(22);
                DateTime curenttime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


                //delete files which are created 1 hour ago
                DeleteOldFiles(dirpath);

                //Retrieve parked candidate data
                List<CandidateDetails> canddata = GetAllParkedCandidate();

                //Process each parked candidate
                canddata.ForEach(x =>
                {

                    // Define log file path for the candidate
                    string logfilename = "log-" + x.SchId + ".txt";
                    string logFilePath = System.IO.Path.Combine(dirpath, logfilename);

                    // Check if the file already exists
                    if (System.IO.File.Exists(logFilePath))
                    {
                        // File exists, delete it
                        System.IO.File.Delete(logFilePath);
                    }

                    // StringBuilder to store log messages
                    StringBuilder sblog = new StringBuilder();

                    // Path to candidate's resume PDF file
                    string folderpath = System.IO.Path.Combine(_configuration.Path, "candidate_resume", x.JobId.ToString() + "/");
                    string pdfFilePath = System.IO.Path.Combine(folderpath, x.ResumePath);
                    FileInfo efile = new FileInfo(pdfFilePath);
                    if (efile.Exists.Equals(true))
                    {
                        sblog.AppendLine(curenttime + " -Resume found for parking clearance.");

                        // Extract text from candidate's resume
                        string key = _configuration.PdfKey;
                        string text = Utility.GetTextFromPDF(pdfFilePath, key);

                        // Extract candidate's  skills from resume
                        List<string> extractedskills = LoadCandidateSkills(text);
                        sblog.AppendLine(curenttime + " -The candidate's skills were retrieved from the resume");

                        // check if candidate is a expert
                        bool isexpert = _technicalDAL.IsExpert(x.Phone.Trim(), x.Email.Trim());
                        if (isexpert)
                        {
                            sblog.AppendLine(curenttime + " - The candidate is an expert, so parking clearance cannot be granted.");
                            //check note added or not for candidate is an expert
                            string enote = "The candidate is an expert. Parking clearance denied";
                            bool isExpertNoteAdded = _technicalDAL.IsScheduleNoteAdded(x.SchId, enote);
                            if (!isExpertNoteAdded)
                            {
                                //Add schedule note
                                var schnote = new ScheduleNote
                                {
                                    SchId = x.SchId,
                                    CreatedBy = "VEmp101",
                                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                                    Notes = enote,
                                    IsExternal = false
                                };
                                _technicalDAL.AddScheduleNotes(schnote);
                            }
                            // Write log to file
                            using (StreamWriter writer = new StreamWriter(logFilePath))
                            {
                                writer.Write(sblog.ToString());
                            }
                            return;
                        }

                        // check if candidate alrady exist
                        bool iscandexist = _technicalDAL.IsCandidateExist(x.Phone.Trim(), x.Email.Trim(), x.JobId);
                        if (iscandexist)
                        {
                            sblog.AppendLine(curenttime + " - The candidate has already appeared, so parking clearance cannot be granted.");
                            //check note added or not for candidate already exist
                            string cnote = "The candidate has already appeared. Parking clearance denied";
                            bool isCandidateExistNoteAdded = _technicalDAL.IsScheduleNoteAdded(x.SchId, cnote);
                            if (!isCandidateExistNoteAdded)
                            {
                                //Add schedule note
                                var schnote = new ScheduleNote
                                {
                                    SchId = x.SchId,
                                    CreatedBy = "VEmp101",
                                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                                    Notes = cnote,
                                    IsExternal = false
                                };
                                _technicalDAL.AddScheduleNotes(schnote);
                            }
                            // Write log to file
                            using (StreamWriter writer = new StreamWriter(logFilePath))
                            {
                                writer.Write(sblog.ToString());
                            }
                            return;
                        }


                        // Check if candidate skills match job requirements
                        var jobskills = GetJobSkills(x.JobId);
                        var matching = WordMatching(jobskills, extractedskills);
                        sblog.AppendLine(curenttime + " -The process of matching skills for parking clearance has begun.");

                        // Check skill matching percentage
                        if (matching < 10)
                        {
                            sblog.AppendLine(curenttime + " -Skills not matched.");

                            //check note added or not for skill mismatch
                            string snote = "Skills mismatch. Parking clearance denied";
                            bool isSkillMisMatchNoteAdded = _technicalDAL.IsScheduleNoteAdded(x.SchId, snote);
                            if (!isSkillMisMatchNoteAdded)
                            {
                                //Add schedule note
                                var schnote = new ScheduleNote
                                {
                                    SchId = x.SchId,
                                    CreatedBy = "VEmp101",
                                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                                    Notes = snote,
                                    IsExternal = false
                                };
                                _technicalDAL.AddScheduleNotes(schnote);
                            }
                            // Write log to file
                            using (StreamWriter writer = new StreamWriter(logFilePath))
                            {
                                writer.Write(sblog.ToString());
                            }
                            return;

                        }
                        else
                        {
                            sblog.AppendLine(curenttime + " -Skills matched.");

                            // Check if candidate is parked
                            int sts = CheckScheduleStatus(x.SchId);
                            if (sts == (int)AllEnum.ScheduleStatusEnum.Parked)
                            {
                                // Retrieve candidate details
                                Candidate cand = GetCandidateDetails(Convert.ToInt32(x.CandidateId));
                                // Check if candidate is already approved
                                if (cand.Status == 1)
                                {
                                    sblog.AppendLine(curenttime + " -The candidate has already been approved.");
                                    // Write log to file
                                    using (StreamWriter writer = new StreamWriter(logFilePath))
                                    {
                                        writer.Write(sblog.ToString());
                                    }
                                    return;
                                }
                                else
                                {
                                    TimeSpan ts = new TimeSpan(0, 0, 0);
                                    // Check if candidate has no available time
                                    if (Convert.ToDateTime(x.AvailableTime).TimeOfDay == ts)
                                    {
                                        sblog.AppendLine(curenttime + " -Unable to find the candidate's available time for parking clearance.");

                                        // Transition candidate to new status
                                        Candidate candidate = new Candidate
                                        {
                                            Name = x.Name,
                                            Email = x.Email,
                                            Phone = x.Phone,
                                            CandidateId = x.CandidateId,
                                            Status = 1,
                                            CountryCode = x.CountryCode
                                        };
                                        ParkedToNewCandidate(candidate);
                                        AddInterviwHistory(x.SchId, "Candidated Approved", "VEmp101", null, 0, (int)ScheduleStatusEnum.New);
                                        UpdateScheduleStatus(x.SchId, AllEnum.ScheduleStatusEnum.New);

                                        //send availability mail to candidate
                                        InterviewSchedule model = new()
                                        {
                                            CandidateId = x.CandidateId,
                                            Id = x.SchId,
                                            JobId = x.JobId
                                        };
                                        CandidateAvailabilityEmail(model);

                                        //availability msg to candidate
                                        string code = Utility.EncodeValue(x.SchId.ToString());
                                        string weblink = _configuration.AbsolutePath + "book-availability?p=" + code.Trim();
                                        JobReqModel jobRequirement = _technicalDAL.GetJobDetails(x.JobId);
                                        string[] cparam = { candidate.Name.Trim(), jobRequirement.CustomerName, jobRequirement.Position, weblink, jobRequirement.CustomerName };
                                        string cphoneno = "+" + candidate.CountryCode.Trim().Replace("+", "") + candidate.Phone.Trim();

                                        var cr = _communicationRepository.WhatsAppSendMsgAPI(cphoneno, "VEmp101", AllEnum.WhatsAppCampaign.CandidateAvailability, cparam, _configuration.WhatsAppOpsUser);

                                        sblog.AppendLine(curenttime + " -The candidate has been approved and transitioned to a new status.");
                                        // Write log to file
                                        using (StreamWriter writer = new StreamWriter(logFilePath))
                                        {
                                            writer.Write(sblog.ToString());
                                        }
                                        return;

                                    }
                                    else if (Convert.ToDateTime(x.AvailableTime).TimeOfDay > ts)
                                    {
                                        sblog.AppendLine(curenttime + " -The candidate's available time found for parking clearance.");

                                        // Process candidate with available time
                                        Candidate candidate = new Candidate
                                        {
                                            CandidateId = Convert.ToInt32(x.CandidateId),
                                            Name = x.Name.Trim(),
                                            Email = x.Email.Trim(),
                                            Phone = x.Phone.Trim(),
                                            CountryCode = x.CountryCode.Trim(),
                                            AvailableTime = Convert.ToDateTime(x.AvailableTime),
                                            TimeZone = x.Timezone,
                                            RelExp = x.RelExp
                                        };
                                        InterviewSchedule intv = _technicalDAL.GetScheduleByCandidateId(candidate.CandidateId);
                                        _technicalDAL.UpdateScheduleStatus(x.SchId, AllEnum.ScheduleStatusEnum.New);
                                        int schId = x.SchId;
                                        string schlog = PerformAutoSchdule(intv, candidate, "VEmp101", schId);
                                        sblog.AppendLine(schlog);
                                        // Write log to file
                                        using (StreamWriter writer = new StreamWriter(logFilePath))
                                        {
                                            writer.Write(sblog.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Log absence of resume
                        sblog.AppendLine(curenttime + " -Resume not found for parking clearance");
                        // Write log to file
                        using (StreamWriter writer = new StreamWriter(logFilePath))
                        {
                            writer.Write(sblog.ToString());
                        }
                    }

                });

            }


        }
        private void DeleteOldFiles(string folderPath)
        {
            //Retrive all files from the folder
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                DateTime creationTime = fileInfo.CreationTime.Date;
                DateTime oneWeekAgo = DateTime.Now.AddDays(-7).Date;

                // Delete all files which are created before 1 weak
                if (creationTime <= oneWeekAgo)
                {
                    System.IO.File.Delete(file);
                }


            }
        }
        private List<CandidateDetails> GetAllParkedCandidate()
        {
            return _technicalDAL.GetAllParkedCandidate();
        }
        private int CheckScheduleStatus(int schid)
        {
            return _technicalDAL.CheckScheduleStatus(schid);
        }
        private int AddInterviwHistory(int schId, string notes, string userId, string expId = null, int spocId = 0, int statusId = 0)
        {
            InterviewHistory history = new InterviewHistory()
            {
                InterviewId = schId,
                Notes = notes,
                UserId = userId,
                ExpertId = null,
                SpocId = 0,
                StatusId = (int)AllEnum.ScheduleStatusEnum.PendingWithClient
            };
            return _technicalDAL.AddInterviwHistory(history);
        }

        private int ParkedToNewCandidate(Candidate candidate)
        {
            return _technicalDAL.ParkedToNewCandidate(candidate);
        }
        private int UpdateScheduleStatus(int schId, ScheduleStatusEnum interviewStatus)
        {
            return _technicalDAL.UpdateScheduleStatus(schId, interviewStatus);
        }
        private List<string> LoadCandidateSkills(string eskills)
        {
            string[] specialCharacters = { "\b", "\f", "\n", "\r", "\t", "\v", "'", "\"", "\\", "/" };
            string pattern = "[" + Regex.Escape(string.Concat(specialCharacters)) + "]";
            string plainText = Regex.Replace(eskills, pattern, "");

            string[] tokens = plainText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            List<string> wordSequences = new List<string>();

            for (int i = 1; i <= 6; i++)
            {
                for (int j = 0; j <= tokens.Length - i; j++)
                {
                    string wordSequence = string.Join(" ", tokens, j, i);
                    wordSequences.Add(wordSequence);
                }
            }

            // Load skills from db

            List<string> normalizeskills = _technicalDAL.GetAllSkills().Select(x => x.Name.ToLower().Trim()).ToList();

            // Load stopwords from a text file
            string stopwordsFilePath = Path.Combine(_configuration.Path, "data/stopword.txt");
            HashSet<string> stopwordsSet = new HashSet<string>(System.IO.File.ReadLines(stopwordsFilePath).Select(line => line.ToLower().Trim()));

            // Filter tokens to include only those that are skills and not stopwords
            List<string> skillset = wordSequences
                .Where(token => normalizeskills.Contains(token.ToLower().Trim()) && !stopwordsSet.Contains(token.ToLower().Trim()))
                .Distinct()
                .ToList();

            return skillset;
        }
        private List<string> GetJobSkills(int jobId)
        {
            return _technicalDAL.GetJobSkills(jobId);
        }
        private int WordMatching(List<string> jobparam, List<string> candparam)
        {

            var mtcCount = 0;
            jobparam.ForEach(z =>
            {
                if (candparam.Contains(z))
                    mtcCount++;
            });
            var percent = 0;
            if (mtcCount > 0)
                percent = (int)Convert.ToDouble((Convert.ToDouble(mtcCount) / Convert.ToDouble(jobparam.Count())) * 100);
            return percent;
        }
        private Candidate GetCandidateDetails(int Id)
        {
            return _technicalDAL.GetCandidateDetails(Id);
        }

        private int CandidateAvailabilityEmail(InterviewSchedule interview)
        {
            // Get candidate and job details
            Candidate candidate = _technicalDAL.GetCandidateDetails(interview.CandidateId);
            JobReqModel jobRequirement = _technicalDAL.GetJobDetails(interview.JobId);


            // Remove style attributes from job description
            string desc = jobRequirement.Description;

            string pattern1 = "style\\s*=\\s*(['\"])(.*?)\\1";
            string cleaneddesc = Regex.Replace(desc, pattern1, "", RegexOptions.IgnoreCase);

            // Load email template
            var path = System.IO.Path.Combine(_configuration.Path, "email_template/candidate/candidate_availability.html");
            string text = System.IO.File.ReadAllText(path);

            // Generate availability web link for candidate availability
            string code = Utility.EncodeValue(interview.Id.ToString());
            string weblink = _configuration.AbsolutePath + "book-availability?p=" + code;

            // Prepare image attachment for company logo
            string imagetext = "";
            System.Net.Mail.Attachment logo = null;
            string logopath = "";
            try
            {
                if (jobRequirement.CustomerImg != null)
                {
                    logopath = System.IO.Path.Combine(_configuration.Path, "customer_image/", jobRequirement.CustomerImg);
                    logo = new System.Net.Mail.Attachment(logopath);
                    imagetext = String.Format(@"<img src=""cid:{0}"" style=""width: 200px;margin: 0px;""/>", logo.ContentId);
                }
            }
            catch (Exception ex)
            {

            }

            // Replace placeholders in the email template
            text = text.Replace("$Candidate$", candidate.Name)
              .Replace("$Company$", jobRequirement.CustomerName)
              .Replace("$weblink$", weblink).Replace("$ImageText$", imagetext).Replace("$jobtext$", cleaneddesc)
              .Replace("$Position$", jobRequirement.Position)
              .Replace("$CustomerWebsite$", jobRequirement.CustomerUrl);

            // Prepare email subject
            string subject = "Interview Schedule Request: " + jobRequirement.CustomerName + " (" + jobRequirement.Position + ")";

            // Get HR emails associated with the candidate
            List<string> hremails = _technicalDAL.GetCandidateOwnerEmails(candidate.CandidateId, jobRequirement.JobId);

            // Get email configuration for the customer
            CustomerEmailConfig emailConfig = this.GetEmailConfig(jobRequirement.CustomerId);

            // Send candidate availability email
            CommunicationRepository.CandidateAvalialbilityEmail(subject, text, candidate.Email, logo, emailConfig, null, hremails);

            return 1;
        }
        private CustomerEmailConfig GetEmailConfig(int cid)
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

        private string PerformAutoSchdule(InterviewSchedule interview, Candidate candidate, string userId, int schId)
        {
            DateTime cur_time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            StringBuilder sb = new StringBuilder();
            JobRequirement jobDetails = _technicalDAL.GetJobRequirement(interview.JobId);
            DateTime schTime = Utility.ConvertDateSourceToDestTimeZone(candidate.TimeZone, Convert.ToDateTime(candidate.AvailableTime), "India Standard Time");

            // Check if client agreement expired
            if (_technicalDAL.ClientAgreementExpired(jobDetails.CustomerId, schTime))
            {
                sb.AppendLine(cur_time + " -AIVEN is unable to initiate scheduling as the customer's agreement has expired.");
                return sb.ToString();
            }

            // Check if the scheduled time is a holiday
            if (_technicalDAL.CheckHoliday(schTime))
            {
                sb.AppendLine(cur_time + " -AIVEN is unable to initiate scheduling as the availabilty date given is a holiday.");
                return sb.ToString();
            }
            sb.AppendLine(cur_time + " -AIVEN started scheduling.");

            // Check if candidate provided available time
            if (candidate.AvailableTime == null)
            {
                sb.AppendLine(cur_time + " -Candidate has not provided their available time.");
                return sb.ToString();
            }

            // Check if scheduled time is in the past
            if (schTime < cur_time)
            {
                sb.AppendLine(cur_time + " -The candidate's provided available time is incorrect.");
                return sb.ToString();
            }

            // Check if scheduled time is within working hours
            TimeSpan startTime = new TimeSpan(8, 0, 0);
            TimeSpan endTime = new TimeSpan(24, 0, 0);

            if (schTime.TimeOfDay < startTime || schTime.TimeOfDay > endTime)
            {
                sb.AppendLine(cur_time + " -Current time is out of the VProPle working hours");
                return sb.ToString();
            }

            // Fetch SPOC based on availability
            int SPOCId = 135;

            sb.AppendLine(cur_time + " -Fetching the SPOC based on availability for " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");

            List<int> lstSpocIds = _technicalDAL.GetHostAvailbilityByTime(schTime);
            if (lstSpocIds.Count() == 0)
            {
                // No available SPOC, adjust scheduling time
                sb.AppendLine(cur_time + " -No SPOC is available on " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                schTime = schTime.AddMinutes(15);
                lstSpocIds = _technicalDAL.GetHostAvailbilityByTime(schTime);
                sb.AppendLine(cur_time + " -Fetching the SPOC based on availability for " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                if (lstSpocIds.Count() == 0)
                {
                    sb.AppendLine(cur_time + " -No SPOC is available on " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                    schTime = schTime.AddMinutes(-30);
                    lstSpocIds = _technicalDAL.GetHostAvailbilityByTime(schTime);
                    sb.AppendLine(cur_time + " -Fetching the SPOC based on availability for " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                }
                if (lstSpocIds.Count() == 0)
                {
                    sb.AppendLine(cur_time + " -No SPOC is available on " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                    schTime = schTime.AddMinutes(15);
                    lstSpocIds = _technicalDAL.GetHostAvailbilityByTime(schTime);
                    sb.AppendLine(cur_time + " -Fetching the SPOC based on availability for " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
                }
            }
            if (lstSpocIds.Count() > 0)
            {
                sb.AppendLine(cur_time + " -The SPOC, " + _technicalDAL.GetSPOCName(lstSpocIds[0]).Trim() + ", has been identified for the interview");
                SPOCId = lstSpocIds[0];
            }
            else
            {
                sb.AppendLine(cur_time + " -No SPOC is available on " + schTime.ToString("dd MMM yy hh:mm tt") + " for scheduling interview");
            }


            sb.AppendLine(cur_time + " -Fetching Experts availability associated with Domain or Customer");

            //Get Customer Selected Experts count.
            var chkCusExpert = _technicalDAL.CheckCustomerExpert(interview.JobId);


            //Get Customer Selected Experts
            var cusExpert = _technicalDAL.GetExpertByJobId(interview.JobId, candidate.Email, candidate.Phone, schTime.ToString()).Where(x => x.ProfileSummary == "Available").ToList();

            // Check if custom experts are required and available
            if (chkCusExpert > 0 && cusExpert.Count() == 0)
            {
                sb.AppendLine(cur_time + " -AIVEN is not able to schedule. Please schedule manually looking into Candidate and Expert availability");
                return sb.ToString();
            }

            // Fetch domain experts available at the scheduled time
            List<TempExpertAvailibity> domainExperts = _technicalDAL.GetExpertAvailbilityByDomain(jobDetails.DomainId, schTime);

            // Filter available experts by intersecting custom and domain experts
            cusExpert = (from p in domainExperts
                         join q in cusExpert on p.ExpertId equals q.Id
                         select q).Distinct().ToList();

            // If no available experts found
            var tempExperts = 0;
            if (cusExpert.Select(x => x.Id).Distinct().Count() == 0)
            {
                sb.AppendLine(cur_time + " -No expert is available during this available time.");
                //Get the expert Count
                sb.AppendLine(cur_time + " -Getting active Experts based on Domain: " + jobDetails.Domain.DomainName);
                tempExperts = _technicalDAL.GetExpertCountBasedOnDomain(jobDetails.DomainId);
                sb.AppendLine(cur_time + " -" + (tempExperts - 1) + "Expert(s) have been found for scheduling interview.");
            }
            else
            {
                sb.AppendLine(cur_time + " -" + cusExpert.Select(x => x.Id).Distinct().Count() + " expert(s) found for scheduling interview.");

                //Filter based on Candidate Experience
                sb.AppendLine(cur_time + " -Filtering experts experience based on candidate Experience: " + candidate.RelExp + "yrs for interview");
                cusExpert = cusExpert.Where(x => x.YrsExp >= candidate.RelExp).ToList();
                sb.AppendLine(cur_time + " -" + cusExpert.Select(x => x.Id).Distinct().Count() + " expert(s) have been found with respect to candidate's experience.");
            }

            string retVal = "";
            string message = "";

            // If available experts found, proceed with scheduling
            if (cusExpert.Count > 0)
            {
                interview.ExpertId = _technicalDAL.GetLessAssignExpertCurrMonth(cusExpert.Select(x => x.Id).Distinct().ToList(), schTime);
                interview.Status = (int)ScheduleStatusEnum.Scheduled;
                interview.CancelReason = null;
                interview.ScheduledOn = schTime;
                interview.VproPleSpoc = SPOCId;
                interview.CreatedBy = userId;
                retVal = AddAutoSchedule(interview);
                sb.AppendLine(cur_time + " -" + retVal);
                sb.AppendLine(cur_time + " -Interview has been successfully scheduled by AIVEN.");
                return sb.ToString();
            }
            else
            {
                // Handle scenarios where there are no available experts
                if (tempExperts >= 5)
                {
                    sb.AppendLine(cur_time + " -As we have more than 5 experts on this domain, then scheduling to VProPle Expert");
                    if ((Convert.ToDateTime(schTime) - cur_time).TotalHours >= 12)
                    {
                        //Add the Dummy Expert
                        interview.ExpertId = "VExp100";
                        interview.ExpertCost = 0;
                        interview.VproPleSpoc = SPOCId;
                        interview.Status = (int)ScheduleStatusEnum.Scheduled;
                        interview.CancelReason = null;
                        interview.CreatedBy = userId;
                        interview.ScheduledOn = schTime;
                        retVal = AddAutoSchedule(interview);
                        sb.AppendLine(cur_time + " -" + retVal);
                        //Add a Record in Need Expert Window
                        ExpertRequrement expertRequrement = new ExpertRequrement();
                        expertRequrement.DomiaId = jobDetails.JobId;
                        expertRequrement.Notes = "Expert Required at " + schTime.ToString("dd MMM, hh:mm tt");
                        expertRequrement.CreatedBy = "VEmp101";
                        expertRequrement.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        expertRequrement.IsActive = true;
                        if (schId != 0)
                        {
                            expertRequrement.SchId = schId;
                        }
                        _technicalDAL.AddExpertRequirements(expertRequrement);
                        sb.AppendLine(cur_time + " -Interview has been successfully scheduled by AIVEN.");
                        return sb.ToString();
                    }
                    else
                    {
                        // Insufficient available time, prompt for manual scheduling
                        sb.AppendLine(cur_time + " -As available time is less than 12 hrs. Please schedule manually.");
                        if (interview.Status == 7)
                            interview.Status = 1;
                        _technicalDAL.UpdateScheduleStatus(interview.Id, (ScheduleStatusEnum)Convert.ToInt32(interview.Status));
                    }
                }
                else
                {
                    // No sufficient experts available for scheduling
                    if (interview.Status == 7)
                        interview.Status = 1;
                    sb.AppendLine(cur_time + " -As we dont have more than 5 experts on this Domain. AIVEN cant Schedule.");
                    _technicalDAL.UpdateScheduleStatus(interview.Id, (ScheduleStatusEnum)Convert.ToInt32(interview.Status));
                }
            }
            // Handle cases where scheduling is not allowed
            if (retVal == "Not Allowed")
            {
                //Add the Dummy Expert
                sb.AppendLine(cur_time + " -Somehow the Current expert is booked with another interview, but Scheduling to VProPle Expert");
                interview.ExpertId = "VExp100";
                interview.ExpertCost = 0;
                interview.VproPleSpoc = SPOCId;
                interview.Status = (int)ScheduleStatusEnum.Scheduled;
                interview.CancelReason = null;
                interview.CreatedBy = userId;
                interview.ScheduledOn = schTime;
                retVal = AddAutoSchedule(interview);
                sb.AppendLine(cur_time + " -" + retVal);
                //Add a Record in Need Expert Window
                ExpertRequrement expertRequrement = new ExpertRequrement();
                expertRequrement.DomiaId = jobDetails.JobId;
                expertRequrement.Notes = "Expert Required at " + Convert.ToDateTime(candidate.AvailableTime).ToString("dd MMM, hh:mm tt");
                expertRequrement.CreatedBy = userId;
                expertRequrement.CreatedOn = (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE)).Date;
                expertRequrement.IsActive = true;
                if (schId != 0)
                {
                    expertRequrement.SchId = schId;
                }
                _technicalDAL.AddExpertRequirements(expertRequrement);
                sb.AppendLine(cur_time + " -Interview has been successfully scheduled by AIVEN.");
                return sb.ToString();
            }

            // Scheduling is not possible, return message
            sb.AppendLine(cur_time + " -AIVEN is not able to schedule. Please schedule manually looking into Candidate and Expert times");
            return sb.ToString();
        }
        private VPEmailConfig GetVPEmailConfig()
        {

            return new VPEmailConfig()
            {
                FromEmail = _configuration.FromEmail,
                HostName = _configuration.HostName,
                EmailPassword = _configuration.EmailPassword
            };

        }
        private string AddAutoSchedule(InterviewSchedule interview)
        {
            InterviewSchedule cur_schedule = null;
            if (interview.CandidateId > 0)
            {
                // Fetch current schedule from the candidate id 
                cur_schedule = _technicalDAL.GetScheduleByCandidateId(interview.CandidateId);
            }

            // Check if the expert is block or not
            if (_technicalDAL.GetExpertAllocationAllowOrNot(interview.ExpertId, interview.ScheduledOn) != "Blocked")
            {
                //GetExpert Details
                Expert exp = _technicalDAL.GetExpertDetailsById(interview.ExpertId);
                //Get candidate Details
                Candidate candidate = _technicalDAL.GetCandidateDetails(interview.CandidateId);
                //Get job Details
                JobReqModel jobRequirement = _technicalDAL.GetJobDetails(interview.JobId);
                string meetingLink = "VProPle_" + jobRequirement.CustomerName.Trim() + "_" + jobRequirement.DomainName.Trim() + "_" + jobRequirement.Position.Trim() + "_" + cur_schedule.Id;
                string meetingSubject = jobRequirement.CustomerName.Trim() + "_" + jobRequirement.DomainName.Trim() + "_" + jobRequirement.Position.Trim() + "_" + candidate.Name + "_" + Convert.ToDateTime(interview.ScheduledOn).ToString("dd MMM yyyy hh:mm tt");

                // If meeting link is not available, generate it
                if (string.IsNullOrEmpty(cur_schedule.Meetinglink))
                {
                    var jsonpath = System.IO.Path.Combine(_configuration.Path, "config/vprople-emails-3a39e208cd1f.json");
                    Event meeting = this.GenerateMeetingLink(jsonpath, Convert.ToDateTime(interview.ScheduledOn), exp.EmailId, meetingLink, "India Standard Time");
                    interview.Meetinglink = meeting.HangoutLink;
                }
                else
                    interview.Meetinglink = cur_schedule.Meetinglink;

                // Set interview code if available, otherwise generate a new one
                if (cur_schedule.InterviewCode != null)
                {
                    interview.InterviewCode = cur_schedule.InterviewCode;
                }
                else
                    interview.InterviewCode = CheckInterviewCode();

                // Calculate expert cost
                interview.ExpertCost = GetExpertCost(candidate.TimeZone, interview.ExpertId, jobRequirement.CustomerId, Convert.ToInt32(exp.CurrentRate));

                // Add schedule details 
                string code = _technicalDAL.AddSchedule(interview);
                string folderPath = System.IO.Path.Combine(_configuration.Path, "candidate_resume/temp", jobRequirement.CustomerId.ToString());

                //handle resume deletion
                if (Directory.Exists(folderPath))
                {
                    if (candidate.ResumePath != null)
                    {
                        string fileNameWithoutExtension = candidate.ResumePath.Split(".pdf")[0];

                        DirectoryInfo dir = new DirectoryInfo(folderPath);
                        var file = dir.GetFiles().ToList().Where(x => x.Name.Contains(fileNameWithoutExtension)).FirstOrDefault();
                        if (file != null)
                        {
                            var filename = file.FullName;
                            System.IO.File.Delete(filename);
                        }
                    }
                }

                // Prepare email template
                var path = System.IO.Path.Combine(_configuration.Path, "email_template/expert/interview_feedback.html");
                string text = System.IO.File.ReadAllText(path);

                // Convert schedule time to expert's timezone
                string expertscheduletime = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), exp.TimeZone).ToString("dd MMM yyyy hh:mm tt");
                string tz = Utility.ShortTimeZone(exp.TimeZone);
                var datetz = expertscheduletime + " (" + tz + ")";

                //Generate questions for candidate if jobtype is coding challanges
                if (jobRequirement.JobType == (int)JobTypeEnum.CodingRound)
                {
                    GenerateCodingChallanges(jobRequirement.JobId, jobRequirement.CodingQuestions, interview.Id, Convert.ToDecimal(candidate.RelExp));
                }

                // Generate unique code
                var res = genrateConfirmationCode(cur_schedule);
                string confirmlinkExpert = _configuration.AbsolutePath + "expert-confirmation/" + Utility.EncodeValue(interview.ExpertId) + "-" + res[0];

                // Replace placeholders in email template
                text = text.Replace("$ExpertName$", exp.Name)
                .Replace("$time$", datetz)
                .Replace("$company$", jobRequirement.CustomerName)
                .Replace("$Position$", jobRequirement.Position)
                 .Replace("$difftime$", res[1]).Replace("$ConfirmLink$", confirmlinkExpert);

                string subject = "VProPle Expert Invite: " + jobRequirement.Position + "-" + datetz + "-" + candidate.Name;


                // Add schedule note
                var schnote = new ScheduleNote
                {
                    SchId = interview.Id,
                    CreatedBy = "VEmp101",
                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                    Notes = "Scheduled By AIVEN",
                    IsExternal = true
                };
                var addNotecand = _technicalDAL.AddScheduleNotes(schnote);

                //Update expert resolved by
                _technicalDAL.UpdateExprtReqBySchId(interview.Id, "VEmp101", TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE));

                // Send email and WhatsApp message to the expert
                if (interview.ExpertId != "VExp100")
                {
                    VPEmailConfig emailConfig = this.GetVPEmailConfig();
                    CommunicationRepository.ExpertEmail(subject, text, exp.EmailId, emailConfig, _configuration.Path, null);
                    string[] param = { exp.Name.Trim(), jobRequirement.Position, datetz, confirmlinkExpert };
                    string phoneno = "+" + exp.CountryCode.Trim().Replace("+", "") + exp.WhatsAppNo.Trim();

                    var r = _communicationRepository.WhatsAppSendMsgAPI(phoneno, "VEmp101", WhatsAppCampaign.ExpertInterviewInvite, param, _configuration.WhatsAppERMUser);
                }

                // Send email and WhatsApp message to the candidate
                if (jobRequirement.IsCandidateEmail)
                    SendEmailtoCandidate(interview);


                // Generate confirmation link for the candidate
                string confirmlinkCand = _configuration.AbsolutePath + "candidate-confirmation/" + res[0];
                DateTime sch_time = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), candidate.TimeZone);
                string ctime = sch_time.ToString("dd MMM yyyy hh:mm tt");
                string ctz = Utility.ShortTimeZone(candidate.TimeZone);
                var cdatetz = ctime + " (" + ctz + ")";
                string[] cparam = { candidate.Name.Trim(), jobRequirement.Position, cdatetz, confirmlinkCand, jobRequirement.CustomerName };
                string cphoneno = "+" + candidate.CountryCode.Trim().Replace("+", "") + candidate.Phone.Trim();

                var cr = _communicationRepository.WhatsAppSendMsgAPI(cphoneno, "VEmp101", WhatsAppCampaign.CandidateInterviewSchedule, cparam, _configuration.WhatsAppOpsUser);

                string notes = "Interview Scheduled On:" + Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), exp.TimeZone).ToString("dd MMM yyyy hh:mm tt") + " SPOC:(" + _technicalDAL.GetSPOCName((int)interview.VproPleSpoc)
                    + ") Expert:(" + exp.Name + ")";

                // Add to interview history  
                this.AddInterviwHistory(interview.Id, notes, interview.CreatedBy, interview.ExpertId, (int)interview.VproPleSpoc, (int)interview.Status);

                //Add notification
                _technicalDAL.AddNotification(new Notification
                {
                    Description = notes,
                    NotificatonTypeId = 6,
                    Title = "Interview Scheduled",
                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE)
                });
                return notes;
            }
            else
            {
                return "Not Allowed";
            }
        }
        private Event GenerateMeetingLink(string keyfilepath, DateTime schOn, string expEmailId, string subject, string timezone)
        {
            try
            {
                // Define the scopes required for accessing Google Calendar API
                string[] Scopes = {
                       CalendarService.Scope.Calendar,
                       CalendarService.Scope.CalendarEvents,
                       CalendarService.Scope.CalendarEventsReadonly
                };

                // Load Google credentials from the key file
                GoogleCredential credential;
                using (var stream = new FileStream(keyfilepath, FileMode.Open, FileAccess.Read))
                {
                    // As we are using admin SDK, we need to still impersonate user who has admin access    
                    //  https://developers.google.com/admin-sdk/directory/v1/guides/delegation    
                    credential = GoogleCredential.FromStream(stream)
                     .CreateScoped(Scopes).CreateWithUser(_configuration.FromEmail);
                }

                // Create Calendar API service.    
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "VProPle Interviews Calander",
                });

                Event evnt = new Event();

                // Define event start and end times
                DateTime start = schOn;
                DateTime end = schOn.AddMinutes(45);

                // Set default timezone if not provided
                if (timezone == null)
                {
                    timezone = "Indian Standard Time";
                }
                TimeZoneInfo curTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

                // Convert start and end times to DateTimeOffset using provided timezone
                var dateStart = new DateTimeOffset(start, curTimeZone.GetUtcOffset(start));
                var dateEnd = new DateTimeOffset(end, curTimeZone.GetUtcOffset(end));
                var startTimeString = dateStart.ToString("o");
                var endTimeString = dateEnd.ToString("o");

                evnt.Start = new EventDateTime()
                {
                    DateTime = dateStart.DateTime
                };

                evnt.End = new EventDateTime()
                {
                    DateTime = dateEnd.DateTime
                };
                evnt.Location = "Google Meet";
                evnt.Summary = subject;

                // Define a request to insert the event
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, evnt, _configuration.FromEmail);
                request.ConferenceDataVersion = 1;
                Event response = request.Execute();
                evnt = new Event();

                // Patch the event to include conference data
                evnt.ConferenceData = new ConferenceData
                {
                    CreateRequest = new CreateConferenceRequest
                    {
                        RequestId = Guid.NewGuid().ToString()
                    }
                };
                EventsResource.PatchRequest patchRequest = new EventsResource.PatchRequest(service, evnt, request.CalendarId, response.Id);
                patchRequest.ConferenceDataVersion = 1;
                response = patchRequest.Execute();

                // Return the updated event
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string CheckInterviewCode()
        {
            string interviewcode = Utility.RandomString(10);
            InterviewSchedule Iscodeexist = _technicalDAL.CheckInterviewCode(interviewcode);
            if (Iscodeexist != null)
            {
                CheckInterviewCode();
            }
            return interviewcode;
        }
        private int GetExpertCost(string timezone, string expertId, int customerId, int expCost)
        {
            int cter = _technicalDAL.GetCustomerCostByCndTimezone(customerId, timezone, expertId);
            if (cter > 0)
                return cter;

            int cte = _technicalDAL.GetCustomerCostByCndTimezone(customerId, null, expertId);
            if (cte > 0)
                return cte;

            return expCost;
        }
        private void GenerateCodingChallanges(int jobId, int questionCount, int schId, decimal candYrs)
        {
            List<int> quids = new List<int>();
            List<int> basic = new List<int>();
            List<int> intmdt = new List<int>();
            List<int> advan = new List<int>();

            // Get coding skills required for the job
            List<LanguageSkillMapping> jobSkills = _technicalDAL.GetCodingSkillsByJobID(jobId);

            // Iterate through each coding skill
            jobSkills.ForEach(x =>
            {
                // Get coding questions for the skill
                List<QuestionBank> questions = _technicalDAL.GetCodingQuestionsBySkill(x.SkillId);

                // Filter questions based on candidate's years of experience and Separate challenges into basic, intermediate, and advanced levels
                if (candYrs <= 3)
                {
                    var allChallanges = questions.Where(y => y.ExperienceId == 1).ToList();
                    basic = allChallanges.Where(y => y.TypeId == 1).Select(y => y.Id).ToList();
                    intmdt = allChallanges.Where(y => y.TypeId == 2).Select(y => y.Id).ToList();
                    advan = allChallanges.Where(y => y.TypeId == 3).Select(y => y.Id).ToList();
                }
                else if (candYrs > 3 && candYrs <= 8)
                {
                    var allChallanges = questions.Where(y => y.ExperienceId == 2).ToList();
                    basic = allChallanges.Where(y => y.TypeId == 1).Select(y => y.Id).ToList();
                    intmdt = allChallanges.Where(y => y.TypeId == 2).Select(y => y.Id).ToList();
                    advan = allChallanges.Where(y => y.TypeId == 3).Select(y => y.Id).ToList();
                }
                else if (candYrs > 8)
                {
                    var allChallanges = questions.Where(y => y.ExperienceId == 3).ToList();
                    basic = allChallanges.Where(y => y.TypeId == 1).Select(y => y.Id).ToList();
                    intmdt = allChallanges.Where(y => y.TypeId == 2).Select(y => y.Id).ToList();
                    advan = allChallanges.Where(y => y.TypeId == 3).Select(y => y.Id).ToList();
                }

                // Distribute the required number of questions evenly among the levels
                if (questionCount % 3 == 0)
                {
                    int noQu = questionCount / 3;
                    quids.AddRange(GenerateQuestions(basic, noQu).ToList());
                    quids.AddRange(GenerateQuestions(intmdt, noQu).ToList());
                    quids.AddRange(GenerateQuestions(advan, noQu).ToList());
                }
                else
                {
                    int noQu = questionCount / 3;
                    quids.AddRange(GenerateQuestions(basic, noQu).ToList());
                    quids.AddRange(GenerateQuestions(intmdt, noQu).ToList());
                    quids.AddRange(GenerateQuestions(advan, questionCount - quids.Count()).ToList());
                }

                // Concatenate question IDs
                if (quids.Count() > 0)
                {
                    string concatenated = string.Join(",", quids);

                    // Create and save ScheduledCodeChallenge
                    ScheduledCodeChallange challange = new ScheduledCodeChallange
                    {
                        ScheduleId = schId,
                        SkillId = x.SkillId,
                        QuestionIds = concatenated,
                        CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE)
                    };
                    _technicalDAL.AddScheduledCodeChallanges(challange);
                    quids = new List<int>();
                }
                //Save into 
            });
        }
        private List<int> GenerateQuestions(List<int> questionsId, int number)
        {
            var indices = new List<int>();
            if (questionsId.Count >= number)
            {
                var random = new Random();
                while (indices.Count < number)
                {
                    int index = random.Next(questionsId.Count);
                    if (questionsId.Count > index)
                    {
                        if (!indices.Contains(questionsId[index]))
                            indices.Add(questionsId[index]);
                    }
                }
            }
            return indices;
        }
        public List<string> genrateConfirmationCode(InterviewSchedule interview)
        {
            // Generate unique code if not already generated
            if (string.IsNullOrEmpty(interview.UniqueCode))
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

            // Calculate time difference between now and scheduled time
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var diff = interview.ScheduledOn - now;

            // Determine confirmation message based on time difference
            string time = "Kindly click the \"Confirm\" button below to confirm your attendance for this interview ASAP <br/> <a target=\"_blank\" style=\"padding: 5px 20px; display: inline-block; text-decoration: none; border-radius: 5px; background: #ff8b00; margin-left: 10px; color: #ffffff; \" href=\"$ConfirmLink$\" >Confirm</a> ";
            if (diff.HasValue && diff.Value.TotalHours >= 4)
            {
                time = "Kindly click the \"Confirm\" button below to confirm your attendance for this interview with in 4 hours of receiving this email.<br/> <a target=\"_blank\" style=\"padding: 5px 20px; display: inline-block; text-decoration: none; border-radius: 5px; background: #ff8b00; margin-left: 10px; color: #ffffff; \" href=\"$ConfirmLink$\" >Confirm</a> ";
            }

            // Return unique code and confirmation message
            List<string> val = new List<string>();
            val.Add(interview.UniqueCode);
            val.Add(time);
            return val;
        }

        public string SendEmailtoCandidate(InterviewSchedule interview, bool isReminder = false)
        {
            // Fetch candidate, job, spoc, and interview schedule details
            Candidate candidate = _technicalDAL.GetCandidateDetails(interview.CandidateId);
            JobReqModel jobRequirement = _technicalDAL.GetJobDetails(interview.JobId);
            Spoc spoc = _technicalDAL.GetSpocDetails((int)interview.VproPleSpoc);
            InterviewSchedule intrvsch = _technicalDAL.GetInterviewSchData(interview.CandidateId);

            // Load email template
            string path = System.IO.Path.Combine(_configuration.Path, "email_template/candidate/interview_invite.html");
            string text = System.IO.File.ReadAllText(path);

            // Initialize variables for logo and attachments
            string imagetext = "";
            System.Net.Mail.Attachment logo = null;
            string logopath = "";
            try
            {
                // Load customer logo if available
                if (!string.IsNullOrEmpty(jobRequirement.CustomerImg))
                {
                    logopath = System.IO.Path.Combine(_configuration.Path, "customer_image/", jobRequirement.CustomerImg);
                    logo = new System.Net.Mail.Attachment(logopath);
                    imagetext = String.Format(@"<img width=""200px"" src=""cid:{0}"" />", logo.ContentId);
                }
            }
            catch (Exception ex)
            {

            }
            string plat_link = _configuration.AbsolutePath + "interview-platform-v1/" + intrvsch.UniqueCode + "/intro";
            if (intrvsch.UniqueCode == null)
                plat_link = interview.Meetinglink;


            // Convert scheduled time to candidate's timezone
            DateTime sch_time = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(interview.ScheduledOn), candidate.TimeZone);
            string cheduletime = sch_time.ToString("dd MMM yyyy hh:mm tt") + " (" + Utility.ShortTimeZone(candidate.TimeZone) + ")";

            // Prepare email subject
            string subject = jobRequirement.CustomerName + " Interview Invite: " + jobRequirement.Position + "-" + cheduletime + "-" + candidate.Name;

            System.Net.Mail.Attachment attachment = null;
            string reptext = "";
            string confirmlinkCand = "";
            List<string> hremails = new List<string>();

            // Generate confirmation link if needed
            if (interview.IsCandidateConfirmByS == false && interview.IsCandidateConfirm == false)
            {
                var res = genrateConfirmationCode(intrvsch);
                confirmlinkCand = _configuration.AbsolutePath + "candidate-confirmation/" + res[0];
                reptext = res[1];
            }


            // Replace placeholders in the email template
            if (isReminder)
            {
                // Replace placeholders in reminder email template
                text = text.Replace("$candidatename$", candidate.Name)
               .Replace("$company$", jobRequirement.CustomerName).Replace("$meetinglink$", plat_link)
              .Replace("$time$", cheduletime)
              .Replace("$position$", jobRequirement.Position)
              .Replace("$jdtext$", jobRequirement.Description).Replace("$title$", subject)
              .Replace("$ImageText$", imagetext).Replace("$CustomerName$", jobRequirement.CustomerName)
              .Replace("$spoc$", spoc.Name + " (" + spoc.Phone.Trim() + ")")
              .Replace("$CustomerWebsite$", jobRequirement.CustomerUrl)
              .Replace("$difftime$", reptext).Replace("$ConfirmLink$", confirmlinkCand);

                text = text.Replace("$Note$", " ");


                subject = "Gentle Reminder: " + subject;
            }
            else
            {
                // Replace placeholders in standard email template
                text = text.Replace("$candidatename$", candidate.Name)
               .Replace("$company$", jobRequirement.CustomerName).Replace("$meetinglink$", plat_link)
              .Replace("$time$", cheduletime)
              .Replace("$position$", jobRequirement.Position)
              .Replace("$jdtext$", jobRequirement.Description).Replace("$title$", subject)
              .Replace("$ImageText$", imagetext).Replace("$CustomerName$", jobRequirement.CustomerName)
              .Replace("$spoc$", spoc.Name + " (" + spoc.Phone.Trim() + ")")
              .Replace("$difftime$", reptext).Replace("$ConfirmLink$", confirmlinkCand)
              .Replace("$CustomerWebsite$", jobRequirement.CustomerUrl);

                text = text.Replace("$Note$", " ");

                // Get HR emails 
                hremails = _technicalDAL.GetCandidateOwnerEmails(candidate.CandidateId, jobRequirement.JobId);

            }

            // Get email configuration for the customer
            CustomerEmailConfig emailConfig = this.GetEmailConfig(jobRequirement.CustomerId);

            // Send email to candidate
            CommunicationRepository.CandidateEmail(subject, text, candidate.Email, logo, logopath, emailConfig, attachment, hremails);

            return "Sucess";
        }
    }


}
