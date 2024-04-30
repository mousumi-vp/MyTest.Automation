using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class WhatsAppLog
    {
        public int Id { get; set; }
        public string ContactNo { get; set; }
        public string CampaignName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Status { get; set; }
        public int? SchId { get; set; }
    }
}
