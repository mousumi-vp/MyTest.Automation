using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class SpeakerTextTemp
    {
        public int Id { get; set; }
        public int SchId { get; set; }
        public string SpeakerTest { get; set; }
        public string FeedbackText { get; set; }
    }
}
