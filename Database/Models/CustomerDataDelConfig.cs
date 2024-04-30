using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerDataDelConfig
    {
        public int ConfigId { get; set; }
        public int CustomerId { get; set; }
        public decimal? Resumes { get; set; }
        public decimal? Videos { get; set; }
        public decimal? Transcript { get; set; }
        public decimal? CandidateImages { get; set; }
        public int? DataRetention { get; set; }
        public string DeletionCycle { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool? CanJobDelete { get; set; }
        public bool? CanCandDelete { get; set; }
        public bool? CanNotesDelete { get; set; }
        public bool? CanHistoryDelete { get; set; }
        public bool? CanResumeDelete { get; set; }
        public bool? CanVideoDelete { get; set; }
        public bool? CanTranscriptDelete { get; set; }
        public bool? CanImageDelete { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
