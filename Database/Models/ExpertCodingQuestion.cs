using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertCodingQuestion
    {
        public int Id { get; set; }
        public int? SchId { get; set; }
        public string Question { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDelivered { get; set; }
    }
}
