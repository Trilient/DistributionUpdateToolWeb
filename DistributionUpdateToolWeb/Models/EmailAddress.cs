﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributionUpdateToolWeb.Models
{
    public class EmailAddress
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Email { get; set; }
    }
}