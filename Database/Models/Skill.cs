using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CustomerExpertSkills = new HashSet<CustomerExpertSkill>();
            ExpertSkills = new HashSet<ExpertSkill>();
            JobSkills = new HashSet<JobSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerExpertSkill> CustomerExpertSkills { get; set; }
        public virtual ICollection<ExpertSkill> ExpertSkills { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }
    }
}
