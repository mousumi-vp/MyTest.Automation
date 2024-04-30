using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertRequrement
    {
        public int Id { get; set; }
        public int DomiaId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int? SchId { get; set; }
        public string ResolveBy { get; set; }
        public DateTime? ResolveOn { get; set; }
    }
}
