using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class OpenAiconfig
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string Apiurl { get; set; }
        public string Model { get; set; }
        public string Prompt { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
