using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Domain
    {
        public Domain()
        {
            ExpertDomains = new HashSet<ExpertDomain>();
            JobRequirements = new HashSet<JobRequirement>();
        }

        public int DomainId { get; set; }
        public string DomainName { get; set; }

        public virtual ICollection<ExpertDomain> ExpertDomains { get; set; }
        public virtual ICollection<JobRequirement> JobRequirements { get; set; }
    }
}
