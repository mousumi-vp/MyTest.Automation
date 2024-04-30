using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerVendor
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
    }
}
