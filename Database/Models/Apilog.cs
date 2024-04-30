using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Apilog
    {
        public long LoggerId { get; set; }
        public string LoggerApi { get; set; }
        public Guid CustomerCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Host { get; set; }
        public string Protocol { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string Scheme { get; set; }
        public string QueryString { get; set; }
        public string IsHttps { get; set; }
        public string RemoteIpAddress { get; set; }
    }
}
