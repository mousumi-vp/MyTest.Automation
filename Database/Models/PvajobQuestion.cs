using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvajobQuestion
    {
        public int Id { get; set; }
        public int JobSkillId { get; set; }
        public int JobId { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int QuestionType { get; set; }
        public int? Language { get; set; }
        public bool? IsActive { get; set; }
    }
}
