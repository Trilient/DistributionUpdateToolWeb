using DistributionUpdateToolWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributionUpdateToolWeb.ViewModels
{
    public class ClientViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<EmailAddress> EmailAddresses { get; set; }
    }
}