using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Customers = new HashSet<Customer>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
