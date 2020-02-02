using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistributionUpdateToolWeb.Models;

namespace DistributionUpdateToolWeb.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext _context;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Clients
        public ViewResult Index()
        {
            var clients = _context.Clients.ToList();

            return View(clients);
        }

        public ActionResult Details(int id)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);
            var emailAddressContext = _context.EmailAddresses;
            List<string> clientDistribution = new List<string>();

            foreach (var email in emailAddressContext)
            {
                if (email.ClientId == client.Id) clientDistribution.Add(email.Email);
            }

            client.EmailDistribution = clientDistribution;

            return View(client);
        }
    }
}