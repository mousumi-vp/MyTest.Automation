using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidateCodingEvalution
    {
        public int Id { get; set; }
        public Guid CandidateCode { get; set; }
        public int FeedbackId { get; set; }
        public int LanguageId { get; set; }
        public string SourceCode { get; set; }
        public int? Status { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string CompilationOutput { get; set; }
        public double? Time { get; set; }
        public long? Memory { get; set; }
        public int? RunCount { get; set; }
        public int? QuestionId { get; set; }
    }
}
