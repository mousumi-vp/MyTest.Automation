using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvainterviewFeedback
    {
        public int Id { get; set; }
        public int VaschId { get; set; }
        public string CustomerExpertId { get; set; }
        public int? Result { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid FeedbackCode { get; set; }
    }
}
