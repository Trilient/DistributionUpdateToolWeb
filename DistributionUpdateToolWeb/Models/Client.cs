using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributionUpdateToolWeb.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}