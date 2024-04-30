using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ScheduleNote
    {
        public int Id { get; set; }
        public int SchId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsExternal { get; set; }
    }
}
