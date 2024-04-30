using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class NotificationReceipt
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public DateTime? ReadOn { get; set; }
        public bool IsSend { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
