using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? PhoneNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsVarified { get; set; }
    }
}
