using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Database.Contracts;
using Database.Models;
using Database.OModels;
using Repository.Contracts;
using Repository.Implimentation;

namespace Repository
{
    public class DailyScheduleStatusMailRepository
    {
        private readonly WorkerSettings _configuration;
        private readonly ITechnicalDAL _technicalDAL;
        private readonly ICommunicationRepository _communicationRepository;

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DailyScheduleStatusMailRepository(ITechnicalDAL technicalDAL,
            IOptions<WorkerSettings> configuration, ICommunicationRepository communicationRepository
            )
        {
            _technicalDAL = technicalDAL;
            _configuration = configuration.Value;
            _communicationRepository = communicationRepository;
        }
        public void TriggerEmailToCandidateOwners()
        {
            // Get todays all scheduled 
            List<ScheduleDetailsList> data = _technicalDAL.GetTodaysSchedules();
            foreach (var owner in data)
            {
                TimeZoneInfo ownerTimeZone = TimeZoneInfo.FindSystemTimeZoneById(owner.TimeZone);

                // Convert current UTC time to ownertimezone
                DateTime ownerTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ownerTimeZone);

                // Check if ownerTime is between 9 AM to 9.10 AM
                if (ownerTime.Hour == 9 && ownerTime.Minute >= 0 && ownerTime.Minute < 10)
                {
                    // Send email to owner
                    SendMailToCandidateOwners(owner.OwnerMail, owner.OwnerName, owner.TimeZone, owner.ScheduleDetails, owner.JobOwnerEmails);
                }
            }

        }

        private void SendMailToCandidateOwners(string ownerMail, string ownerName, string ownerTimeZone, List<ScheduleDetails> scheduleDetails,List<string> jobowner)
        {
            //Read HTML Email template from the path
            string htmlFilePath = Path.Combine(_configuration.Path, "email_template/customer/schedule-status-to-candOwner.html");
            string htmlContent = File.ReadAllText(htmlFilePath);

            //Replace owner name from the template
            htmlContent = htmlContent.Replace("$OwnerName$", ownerName);
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string convertedCurrentDate = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(currentDateTime), ownerTimeZone).ToString("dd MMM yyyy");

            htmlContent = htmlContent.Replace("$date$", convertedCurrentDate);
            var shortTimeZone = Utility.ShortTimeZone(ownerTimeZone);
            htmlContent = htmlContent.Replace("$Timezone$", shortTimeZone);
            var subject = "Early Alert for Scheduled Interviews Today (" + convertedCurrentDate + ") - " + ownerTimeZone;

            //Create schedule data table dynamically 
            string insertRow = "";
            scheduleDetails.ForEach(x =>
            {
                var convertedInterviewTime = Utility.ConvertDateSourceToDestTimeZone("India Standard Time", Convert.ToDateTime(x.InterviewOn), ownerTimeZone).ToString("hh:mm tt");
                insertRow += "<tr>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'>" + "VJ-0"+x.JobId+"-"+x.Position + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'>" + x.CandName + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'>" + x.CandEmail + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'>" + x.CandPhone + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'>" + convertedInterviewTime + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'> " + x.Status + "</td>";
                insertRow += "<td style='border: groove;font-family: Verdana !important; font-size: 12px !important; text-align: center !important'> " + x.ScheduleCount + "</td>";
                insertRow += "</tr>";
            });
            // insert table rows in the template
            htmlContent = htmlContent.Replace("$Rows$", insertRow);
            // get email config
            var emailConfig = GetVPEmailConfig();
           
            // send email
            CommunicationRepository.CandOwnerScheduleStatusEmail(subject, htmlContent, ownerMail, emailConfig, _configuration.Path, null, jobowner);
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
