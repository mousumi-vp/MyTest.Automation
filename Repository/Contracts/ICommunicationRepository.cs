using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.OModels;

namespace Repository.Contracts
{
    public interface ICommunicationRepository
    {
        string WhatsAppSendMsgAPI(string phoneno, string userId, string campaign, string[] param, string wauser, int schid = 0);
        string CandidateEmailAll(string subject, string text, string email, Attachment logo, string logopath, CustomerEmailConfig emailConfig, Attachment attachment, List<string> hremails);
        string EmailAllExpert(string subject, string messageBody, string emailId, VPEmailConfig emailConfig, string path, Attachment attachment = null);
        //void CandOwnerScheduleStatusEmail(string subject, string htmlContent, string ownerMail, VPEmailConfig emailConfig, string path, object value);
        //void CandOwnerScheduleStatusEmail(string subject, string messageBody, string emailId, VPEmailConfig emailConfig, string path, Attachment attachment = null);
    }
}
