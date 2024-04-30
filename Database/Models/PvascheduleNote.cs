using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvascheduleNote
    {
        public int Id { get; set; }
        public int VaschId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
