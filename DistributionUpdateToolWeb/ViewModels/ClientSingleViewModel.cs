using DistributionUpdateToolWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributionUpdateToolWeb.ViewModels
{
    public class ClientSingleViewModel
    {
        public Client Client { get; set; }
        public EmailAddress EmailAddress { get; set; }
    }
}