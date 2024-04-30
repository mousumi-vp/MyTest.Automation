using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class JobCalibrationSchedule
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public string ExpertId { get; set; }
        public string Meetinglink { get; set; }
        public DateTime? CalibrationOn { get; set; }
        public string EmailText { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? Status { get; set; }
        public bool IsVarified { get; set; }
        public int? Amount { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaidBy { get; set; }
        public bool IsPaid { get; set; }
        public string Note { get; set; }
        public string ExpertFeedback { get; set; }
        public int? ExpertRating { get; set; }
    }
}
