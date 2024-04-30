using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class QualityChecklist
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public bool? IsChecklist { get; set; }
        public bool? IsEnhanced { get; set; }
        public bool? IsCorrectTranscript { get; set; }
        public bool? IsCorrectVideo { get; set; }
        public bool? IsCorrectCandName { get; set; }
        public string Duration { get; set; }
        public bool? IsIncognito { get; set; }
        public bool? IsMatchLipsMovement { get; set; }
        public string LipsMisMatchTime { get; set; }
        public bool? FeedBackRecvFourHour { get; set; }
        public bool? IsWeakExpert { get; set; }
        public bool? IsUseCodingPltfrm { get; set; }
        public bool? IsFullScreenShare { get; set; }
        public bool? ClientNote { get; set; }
        public bool? UnwantedQues { get; set; }
        public bool? ProxyCandidate { get; set; }
        public bool? DetailedFeedback { get; set; }
        public string CoQa { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string SubmittedBy { get; set; }
    }
}
