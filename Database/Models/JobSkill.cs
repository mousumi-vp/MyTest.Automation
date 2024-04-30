using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class JobSkill
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int SkillId { get; set; }
        public bool? IsActive { get; set; }

        public virtual JobRequirement Job { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
