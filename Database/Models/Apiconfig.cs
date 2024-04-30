using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Apiconfig
    {
        public int Id { get; set; }
        public string Apiname { get; set; }
        public string Apitype { get; set; }
    }
}
