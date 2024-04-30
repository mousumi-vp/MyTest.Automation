using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Mcq
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CodeSnippet { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? SkillId { get; set; }
        public int? CustomerId { get; set; }
        public int? JobId { get; set; }
    }
}
