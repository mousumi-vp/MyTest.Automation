using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvauserVerification
    {
        public int Id { get; set; }
        public int VaschId { get; set; }
        public string Ip { get; set; }
        public string BrowseName { get; set; }
        public string UserAgent { get; set; }
        public int? MouseMovementCount { get; set; }
    }
}
