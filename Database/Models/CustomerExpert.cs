using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerExpert
    {
        public CustomerExpert()
        {
            CustomerExpertSkills = new HashSet<CustomerExpertSkill>();
        }

        public string Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int Experience { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<CustomerExpertSkill> CustomerExpertSkills { get; set; }
    }
}
