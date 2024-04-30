using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class QuestionBank
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public string Text { get; set; }
        public int TypeId { get; set; }
        public int? ExperienceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsCoding { get; set; }
        public int? CustomerId { get; set; }
    }
}
