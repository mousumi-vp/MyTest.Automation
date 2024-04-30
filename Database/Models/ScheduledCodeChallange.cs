using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ScheduledCodeChallange
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int SkillId { get; set; }
        public string QuestionIds { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
