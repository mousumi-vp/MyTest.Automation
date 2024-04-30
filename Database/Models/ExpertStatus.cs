using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertStatus
    {
        public ExpertStatus()
        {
            Experts = new HashSet<Expert>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Expert> Experts { get; set; }
    }
}
