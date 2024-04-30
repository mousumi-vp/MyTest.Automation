using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CodingQuestionBank
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public int? ClientId { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
