using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class BlockedCompany
    {
        public string ExpertId { get; set; }
        public string CompanyName { get; set; }

        public virtual Expert Expert { get; set; }
    }
}
