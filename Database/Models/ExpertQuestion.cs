using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertQuestion
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
