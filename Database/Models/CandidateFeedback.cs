using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidateFeedback
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int ScheduleId { get; set; }
        public string MandatoryFeedback { get; set; }
        public string CompFeedback { get; set; }
        public string SoftSkillsFeedback { get; set; }
        public int? Status { get; set; }
        public string QcuserId { get; set; }
        public string OneWayVideoPath { get; set; }
        public string FullVideoPath { get; set; }
        public string MessageToVp { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? InterviewerRating { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string AdditionalComment { get; set; }
        public decimal? Score { get; set; }
        public int CodingTestStatus { get; set; }
        public string TotalDuration { get; set; }
        public DateTime? CodeTestStartOn { get; set; }
        public DateTime? CodeTestEndOn { get; set; }
        public int? FinalSelection { get; set; }
        public string Notes { get; set; }
        public string TranscriptPath { get; set; }
        public string CommentToQa { get; set; }
        public bool IsNewFeedback { get; set; }
        public string FinalRemarks { get; set; }
        public DateTime? CustomerFeedbackOn { get; set; }
        public decimal? InterviewDuration { get; set; }
        public string CuratedQuestionCount { get; set; }
    }
}
