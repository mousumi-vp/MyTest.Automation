using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Contracts;
using Repository.Contracts;
using Database.Implementation;
using Microsoft.Extensions.Options;
using Database.OModels;

namespace Repository.Implimentation
{
    public class CommunicationRepository : ICommunicationRepository
    {
       
        private readonly ITechnicalDAL _technicalDAL;
        private readonly WorkerSettings _workerSettings;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public CommunicationRepository(IOptions<WorkerSettings> workerSettings, ITechnicalDAL technicalDAL)
        {
            _technicalDAL = technicalDAL;
            _workerSettings = workerSettings.Value;
        }
        public string WhatsAppSendMsgAPI(string phoneno, string userId, string campaign, string[] param, string wauser, int schid = 0)
        {
           
            var username = _technicalDAL.GetEmployeesDetails(userId);
            var Path = _workerSettings.WhatsAppAPIPath;
            string url = string.Format("" + Path + "campaign/go2market/api/v2");
            WebRequest requestObjPost = WebRequest.Create(url);
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";
            requestObjPost.GetRequestStreamAsync().Wait();
            var Key = _workerSettings.WhatsAppAPIKey;
            string[] taglst = { username.FirstName.Trim() + " " + username.LastName.Trim() };
            string attributeval = wauser == "vp_ops_whatsapp" ? "Candidate" : wauser == "vp_erm_whatsapp" ? "Expert" : "";
            var requestPayload = new
            {
                apiKey = Key,
                campaignName = campaign,
                destination = phoneno,
                userName = wauser,
                templateParams = param,
                tags = taglst,
                attributes = new
                {
                    MessageTo = attributeval
                }

            };

            string postData = JsonConvert.SerializeObject(requestPayload);
            try
            {
                using (var StreamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
                {
                    StreamWriter.Write(postData);
                    StreamWriter.Flush();
                    StreamWriter.Close();
                    var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();
                    using (var streamreader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result2 = streamreader.ReadToEnd();
                        dynamic jsonObject = JObject.Parse(result2);

                        // Access the values
                        bool success = jsonObject.success;
                        if (success == true)
                        {
                            WhatsAppLog obj = new WhatsAppLog();
                            obj.Status = true;
                            obj.CampaignName = campaign;
                            obj.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            obj.CreatedBy = userId;
                            obj.ContactNo = phoneno;
                            obj.SchId = schid;
                            _technicalDAL.AddWhatsAppLog(obj);
                            return "Message Sent Successfully to " + param[0] + ".";
                        }
                        else
                        {
                            return "Message Not Sent " + param[0] + ".";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return "Message Not Sent to " + param[0] + " Check country code/phone no for this expert .";
            }

        }
        public static void CandidateAvalialbilityEmail(string subject, string messageBody, string emailId, Attachment logo, CustomerEmailConfig emailConfig, Attachment attachment = null, List<string> hremail = null)
        {
            string msg = string.Empty;
            
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = emailConfig.HostName,
                Port = emailConfig.PortNo,
                EnableSsl = emailConfig.EnableSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.Password)
            };
            MailMessage mailmessage = new MailMessage
            {
                Priority = MailPriority.High,
                From = fromAddress,
                Subject = subject,
                IsBodyHtml = true,
                Body = messageBody
            };
            mailmessage.To.Add(emailId);
            mailmessage.From = fromAddress;
            if (hremail != null)
            {
                try
                {
                    foreach (string s in hremail)
                    {
                        if (!s.Contains("support", StringComparison.OrdinalIgnoreCase))
                        {
                            if (s.Trim() != "")
                                mailmessage.CC.Add(new MailAddress(s.Trim()));
                        }
                    }
                }
                catch (Exception) { }
            }
            else
            {
                //return;
            }

            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logo != null)
                mailmessage.Attachments.Add(logo);
            try
            {
                smtpClient.Send(mailmessage);
            }
            catch (Exception ex) { }
        }

        public static void ExpertEmail(string subject, string messageBody, string emailId,VPEmailConfig emailConfig,string path, Attachment attachment = null)
        {
            MailMessage mailmessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            mailmessage.Priority = MailPriority.High;
            mailmessage.From = fromAddress;

            string imagetextvp = ""; string imagetextsocial = "";
            System.Net.Mail.Attachment logovp = null;
            System.Net.Mail.Attachment logosocial = null;
            string logopathvp = ""; string logopathsocial = "";
            try
            {

                logopathvp = System.IO.Path.Combine(path, "img/logo-blue.png");
                logovp = new System.Net.Mail.Attachment(logopathvp);
                imagetextvp = String.Format(@"<img width=""200px"" src=""cid:{0}"" />", logovp.ContentId);
                messageBody = messageBody.Replace("$vp_logo$", imagetextvp);
                string[] logos = { "facebook", "linkedin", "twitter", "instagram" };

                foreach (string lg in logos)
                {
                    logopathsocial = Path.Combine(path, $"img/social-media/{lg}.png");
                    logosocial = new System.Net.Mail.Attachment(logopathsocial);
                    imagetextsocial = $"<img src=\"cid:{logosocial.ContentId}\" />";
                    messageBody = messageBody.Replace($"${lg}_logo$", imagetextsocial);
                    if (logosocial != null)
                        mailmessage.Attachments.Add(logosocial);
                }
            }
            catch (Exception ex)
            {

            }
            if (emailId != null)
            {
                string[] emails = emailId.Split(',');
                if (emails.Length > 0)
                {
                    foreach (string s in emails)
                    {
                        if (s.Trim() != "")
                            mailmessage.To.Add(new MailAddress(s.Trim()));
                    }
                }
                else
                    mailmessage.To.Add(emailId);
            }
            mailmessage.Subject = subject;
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = messageBody;
            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logovp != null)
                mailmessage.Attachments.Add(logovp);

            smtpClient.Host = emailConfig.HostName;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.EmailPassword);
            try
            {
                smtpClient.Send(mailmessage);
            }
            catch (Exception ex) { }
        }

        public static void CandidateEmail(string subject, string messageBody, string emailId, Attachment logo, string filename, CustomerEmailConfig emailConfig, Attachment attachment = null, List<string> hremail = null)
        {
           
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            string msg = string.Empty;
            SmtpClient smtpClient = new SmtpClient
            {
                Host = emailConfig.HostName,
                Port = emailConfig.PortNo,
                EnableSsl = emailConfig.EnableSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.Password)
            };
            MailMessage mailmessage = new MailMessage
            {
                Priority = MailPriority.High,
                From = fromAddress,
                Subject = subject,
                IsBodyHtml = true,
                Body = messageBody
            };
            mailmessage.To.Add(emailId);

            if (logo != null)
            {
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);
                LinkedResource inline = new LinkedResource(filename, MediaTypeNames.Image.Jpeg);
                inline.ContentId = Guid.NewGuid().ToString();
                avHtml.LinkedResources.Add(inline);
                mailmessage.AlternateViews.Add(avHtml);
            }
            if (hremail != null)
            {
                try
                {
                    foreach (string s in hremail)
                    {
                        if (!s.Contains("support", StringComparison.OrdinalIgnoreCase))
                        {
                            if (s.Trim() != "")
                                mailmessage.CC.Add(new MailAddress(s.Trim()));
                        }

                    }
                }
                catch (Exception) { }
            }
            else
            {
                //return;
            }

            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logo != null)
                mailmessage.Attachments.Add(logo);


            try
            {
                smtpClient.Send(mailmessage);
            }
            catch (Exception ex) { }
        }
        public string CandidateEmailAll(string subject, string messageBody, string emailId, Attachment logo, string filename, CustomerEmailConfig emailConfig, Attachment attachment = null, List<string> hremail = null)
        {
            string msg = string.Empty;
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = emailConfig.HostName,
                Port = emailConfig.PortNo,
                EnableSsl = emailConfig.EnableSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.Password)
            };
            MailMessage mailmessage = new MailMessage
            {
                Priority = MailPriority.High,
                From = fromAddress,
                Subject = subject,
                IsBodyHtml = true,
                Body = messageBody
            };
            mailmessage.To.Add(emailId);
            mailmessage.From = fromAddress;

            //mailmessage.CC.Add(new MailAddress(BL.Common.CCEmailAddress));
            if (logo != null)
            {
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);
                LinkedResource inline = new LinkedResource(filename, MediaTypeNames.Image.Jpeg);
                inline.ContentId = Guid.NewGuid().ToString();
                avHtml.LinkedResources.Add(inline);
                mailmessage.AlternateViews.Add(avHtml);
            }
            if (hremail != null)
            {
                foreach (string s in hremail)
                {
                    if (!s.Contains("support", StringComparison.OrdinalIgnoreCase))
                    {
                        if (s.Trim() != "")
                            mailmessage.CC.Add(new MailAddress(s.Trim()));
                    }
                }
            }
            else
            {
               
            }


            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logo != null)
                mailmessage.Attachments.Add(logo);

            try
            {
                smtpClient.Send(mailmessage);
                return "Reminder Email Sent Successfully to Candidate " + emailId;
            }
            catch (Exception ex)
            {
                return "Reminder Email failed while sending to Candidate " + emailId;
            }
        }
        public  string EmailAllExpert(string subject, string messageBody, string emailId, VPEmailConfig emailConfig, string path, Attachment attachment = null)
        {
            MailMessage mailmessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            mailmessage.Priority = MailPriority.High;
            mailmessage.From = fromAddress;
            string imagetextvp = ""; string imagetextsocial = "";
            System.Net.Mail.Attachment logovp = null;
            System.Net.Mail.Attachment logosocial = null;
            string logopathvp = ""; string logopathsocial = "";
            try
            {

                logopathvp = System.IO.Path.Combine(path, "img/logo-blue.png");
                logovp = new System.Net.Mail.Attachment(logopathvp);
                imagetextvp = String.Format(@"<img width=""200px"" src=""cid:{0}"" />", logovp.ContentId);
                messageBody = messageBody.Replace("$vp_logo$", imagetextvp);
                string[] logos = { "facebook", "linkedin", "twitter", "instagram" };

                foreach (string lg in logos)
                {
                    logopathsocial = Path.Combine(path, $"img/social-media/{lg}.png");
                    logosocial = new System.Net.Mail.Attachment(logopathsocial);
                    imagetextsocial = $"<img src=\"cid:{logosocial.ContentId}\" />";
                    messageBody = messageBody.Replace($"${lg}_logo$", imagetextsocial);
                    if (logosocial != null)
                        mailmessage.Attachments.Add(logosocial);
                }
            }
            catch (Exception ex)
            {

            }
            if (emailId != null)
            {
                string[] emails = emailId.Split(',');
                if (emails.Length > 0)
                {
                    foreach (string s in emails)
                    {
                        if (s.Trim() != "")
                            mailmessage.To.Add(new MailAddress(s.Trim()));
                    }
                }
                else
                    mailmessage.To.Add(emailId);
            }
            mailmessage.Subject = subject;
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = messageBody;
            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logovp != null)
                mailmessage.Attachments.Add(logovp);
            smtpClient.Host = emailConfig.HostName; 
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.EmailPassword);
            try
            {
                smtpClient.Send(mailmessage);
                return "Reminder Email Sent to Expert " + emailId;
            }
            catch (Exception ex)
            {
                return "Reminder Email failed while sending to Expert " + emailId;
            }

        }
        public static void CandOwnerScheduleStatusEmail(string subject, string messageBody, string emailId, VPEmailConfig emailConfig, string path, Attachment attachment = null, List<string> jobOwneremail = null)
        {
            
            MailMessage mailmessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailAddress fromAddress = new MailAddress(emailConfig.FromEmail);
            mailmessage.Priority = MailPriority.High;
            mailmessage.From = fromAddress;

            string imagetextvp = ""; string imagetextsocial = "";
            System.Net.Mail.Attachment logovp = null;
            System.Net.Mail.Attachment logosocial = null;
            string logopathvp = ""; string logopathsocial = "";
            try
            {

                logopathvp = System.IO.Path.Combine(path, "img/logo-blue.png");
                logovp = new System.Net.Mail.Attachment(logopathvp);
                imagetextvp = String.Format(@"<img width=""160px"" src=""cid:{0}"" />", logovp.ContentId);
                messageBody = messageBody.Replace("$vp_logo$", imagetextvp);
                string[] logos = { "facebook", "linkedin", "twitter", "instagram" };

                foreach (string lg in logos)
                {
                    logopathsocial = Path.Combine(path, $"img/social-media/{lg}.png");
                    logosocial = new System.Net.Mail.Attachment(logopathsocial);
                    imagetextsocial = $"<img src=\"cid:{logosocial.ContentId}\" />";
                    messageBody = messageBody.Replace($"${lg}_logo$", imagetextsocial);
                    if (logosocial != null)
                        mailmessage.Attachments.Add(logosocial);
                }
            }
            catch (Exception ex)
            {

            }
            if (jobOwneremail != null)
            {
                foreach (string s in jobOwneremail)
                {
                    if (!s.Contains("support", StringComparison.OrdinalIgnoreCase))
                    {
                        if (s.Trim() != "")
                            mailmessage.CC.Add(new MailAddress(s.Trim()));
                    }
                }
            }
            else
            {

            }

            mailmessage.To.Add(emailId);
            mailmessage.Subject = subject;
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = messageBody;
            if (attachment != null)
                mailmessage.Attachments.Add(attachment);
            if (logovp != null)
                mailmessage.Attachments.Add(logovp);

            smtpClient.Host = emailConfig.HostName;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailConfig.FromEmail, emailConfig.EmailPassword);
            try
            {
                smtpClient.Send(mailmessage);
            }
            catch (Exception ex)
            { 

            }
        }
    }

}
