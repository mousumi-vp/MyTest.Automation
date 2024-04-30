using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ScheduleQuestionsAsk
    {
        public int Id { get; set; }
        public int SchId { get; set; }
        public string Text { get; set; }
    }
}
