using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerTimeZone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string TimeZone { get; set; }
    }
}
