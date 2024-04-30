using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidateCuratedFeedbackHistory
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int VpcurateJdid { get; set; }
        public int Score { get; set; }
    }
}
