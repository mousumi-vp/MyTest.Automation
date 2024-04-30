using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Pvaschedule
    {
        public int VaschId { get; set; }
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public int? Status { get; set; }
        public string UniqueId { get; set; }
        public DateTime? ScheduledOn { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string VideoLink { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string CandidateRemarks { get; set; }
        public decimal? MarksObtained { get; set; }
        public decimal? TotalMarks { get; set; }
        public int? Result { get; set; }
        public string ResultsRemarks { get; set; }
        public string UniCode { get; set; }
        public string Otp { get; set; }
        public bool IsOtpVerified { get; set; }
        public bool IsCandidateAgreed { get; set; }
        public bool IsMediaVerified { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
