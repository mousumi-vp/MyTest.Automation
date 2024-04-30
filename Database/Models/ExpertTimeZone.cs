using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertTimeZone
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string TimeZone { get; set; }
    }
}
