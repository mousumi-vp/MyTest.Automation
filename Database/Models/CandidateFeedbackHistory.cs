using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidateFeedbackHistory
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string MandatoryFeedback { get; set; }
        public string CompFeedback { get; set; }
        public string SoftSkillsFeedback { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal? Score { get; set; }
        public int? Status { get; set; }
        public int? FinalSelection { get; set; }
        public string Notes { get; set; }
        public string FinalRemarks { get; set; }
    }
}
