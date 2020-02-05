using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistributionUpdateToolWeb.Models;

namespace DistributionUpdateToolWeb.ViewModels
{
    public class ClientFullViewModel
    {
        public Client Client { get; set; }
        public ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}