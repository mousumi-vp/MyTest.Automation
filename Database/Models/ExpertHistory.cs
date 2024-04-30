using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertHistory
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
