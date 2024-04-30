using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerCodingQuestion
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public bool IsCoding { get; set; }
        public string Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
