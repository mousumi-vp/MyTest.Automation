using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkerSettings
    {
        public string PdfKey { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string AbsolutePath { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string EmailPassword { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string WhatsAppAPIKey { get; set; } = string.Empty;
        public string WhatsAppAPIPath { get; set; } = string.Empty;
        public string WhatsAppOpsUser { get; set; } = string.Empty;
        public string WhatsAppERMUser { get; set; } = string.Empty;
    }
}
