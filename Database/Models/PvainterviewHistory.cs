using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvainterviewHistory
    {
        public int Id { get; set; }
        public int VaschId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public int? StatusId { get; set; }
    }
}
