﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class EmailCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
