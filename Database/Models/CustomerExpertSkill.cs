using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerExpertSkill
    {
        public int Id { get; set; }
        public string CustomerExpertId { get; set; }
        public int SkillId { get; set; }

        public virtual CustomerExpert CustomerExpert { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
