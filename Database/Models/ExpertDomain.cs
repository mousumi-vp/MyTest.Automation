using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertDomain
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public int DomainId { get; set; }

        public virtual Domain Domain { get; set; }
        public virtual Expert Expert { get; set; }
    }
}
