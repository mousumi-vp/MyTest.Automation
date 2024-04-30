using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertSkill
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public int SkillId { get; set; }
        public double? RelevantYears { get; set; }

        public virtual Expert Expert { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
