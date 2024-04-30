using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PlatformTracker
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PageId { get; set; }
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
