using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.OModels
{
    public class ScheduleDetailsList
    {
        public string OwnerMail { get; set; }
        public string OwnerName { get; set; }
        public string TimeZone { get; set; }
        public List<string> JobOwnerEmails { get; set; }

        public List<ScheduleDetails> ScheduleDetails { get; set; }
    }
    public class ScheduleDetails
    {
        public int SchId { get; set; }
        public int JobId { get; set; }
        public string Position { get; set; }
        public string CandName { get; set; }
        public string CandPhone { get; set; }
        public string CandEmail { get; set; }
        public DateTime InterviewOn { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }
        public string ScheduleCount { get; set; }

    }
}
