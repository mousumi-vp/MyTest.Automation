using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Pvaquestion
    {
        public int Id { get; set; }
        public int VaschId { get; set; }
        public int PvajobQuestionId { get; set; }
        public int? QuestionType { get; set; }
        public string Answer { get; set; }
        public DateTime? SaveOn { get; set; }
        public DateTime? VerifyOn { get; set; }
        public string VerifyBy { get; set; }
        public int? VerifyAnswer { get; set; }
        public string Comments { get; set; }
        public int? RunCount { get; set; }
        public string OutPut { get; set; }
        public int? CodeTestStatus { get; set; }
    }
}
