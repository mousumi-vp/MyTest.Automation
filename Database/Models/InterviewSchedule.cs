using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class InterviewSchedule
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public string ExpertId { get; set; }
        public DateTime? ScheduledOn { get; set; }
        public int? Status { get; set; }
        public int? VproPleSpoc { get; set; }
        public string Meetinglink { get; set; }
        public string CancelReason { get; set; }
        public int? ExpertCost { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FeedbackOn { get; set; }
        public DateTime? MediaUploadOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string CancelledBy { get; set; }
        public string CompletedBy { get; set; }
        public bool? IsPaid { get; set; }
        public string PaidBy { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaymentRemark { get; set; }
        public string InterviewCode { get; set; }
        public string QualityAssigned { get; set; }
        public bool IsVideoDeleted { get; set; }
        public DateTime? EnhancedOn { get; set; }
        public string ChecklistAssignTo { get; set; }
        public DateTime? ChecklistDoneOn { get; set; }
        public bool IsCandidateConfirm { get; set; }
        public bool IsExpertConfirm { get; set; }
        public string UniqueCode { get; set; }
        public bool IsCandidateConfirmByS { get; set; }
        public bool IsExpertConfirmByS { get; set; }
        public int CodeRunCount { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Expert Expert { get; set; }
        public virtual Spoc VproPleSpocNavigation { get; set; }
    }
}
