using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class DeletionLog
    {
        public int Id { get; set; }
        public int? ConfigId { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public int? DelCandidateNotesCount { get; set; }
        public int? DelCandidateHistoryCount { get; set; }
        public int? DelResumesCount { get; set; }
        public decimal? DelResumesSize { get; set; }
        public int? DelVideosCount { get; set; }
        public decimal? DelVideosSize { get; set; }
        public int? DelTranscriptCount { get; set; }
        public decimal? DelTranscriptSize { get; set; }
        public int? DelCandidateImgsCount { get; set; }
        public decimal? DelCandidateImgsSize { get; set; }
        public int? Status { get; set; }
    }
}
