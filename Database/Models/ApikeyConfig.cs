using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ApikeyConfig
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Apikey { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? DevelopmentFee { get; set; }
    }
}
