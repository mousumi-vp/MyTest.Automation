using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class HolidayList
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string HolidayName { get; set; }
        public int Type { get; set; }
    }
}
