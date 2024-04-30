using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Currency
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
