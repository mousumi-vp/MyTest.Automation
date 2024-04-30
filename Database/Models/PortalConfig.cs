using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PortalConfig
    {
        public int Id { get; set; }
        public string Configuration { get; set; }
        public string Value { get; set; }
    }
}
