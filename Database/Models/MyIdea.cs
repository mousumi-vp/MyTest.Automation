using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class MyIdea
    {
        public int Id { get; set; }
        public string Idea { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public string AdminComments { get; set; }
        public string CommentedBy { get; set; }
    }
}
