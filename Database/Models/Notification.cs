using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationReceipts = new HashSet<NotificationReceipt>();
        }

        public int Id { get; set; }
        public int NotificatonTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual NotificationType NotificatonType { get; set; }
        public virtual ICollection<NotificationReceipt> NotificationReceipts { get; set; }
    }
}
