using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerApi
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Apiid { get; set; }
    }
}
