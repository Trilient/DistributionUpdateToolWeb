using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistributionUpdateToolWeb.Models;
using DistributionUpdateToolWeb.ViewModels;

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

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Preview(Client client, FormCollection collection)
        {
            // Receive data from form submission and create a list of EmailAddress objects to populate
            string textDistro = collection["TextEmailDistro"];
            string[] emails = textDistro.Split(';');
            List<EmailAddress> emailDistro = new List<EmailAddress>();

            // Populate data
            if (ModelState.IsValid)
            {
                client = _context.Clients.Add(client);
                _context.SaveChanges();

                foreach (var address in emails)
                {
                    EmailAddress email = new EmailAddress { ClientId = client.Id, Email = address };
                    _context.EmailAddresses.Add(email);
                    emailDistro.Add(email);
                }

                _context.SaveChanges();
            } 
            else
            {
                return HttpNotFound();
            }

            // Pass data into the view model
            var viewModel = new ClientViewModel
            {
                Client = client,
                EmailAddresses = emailDistro
            };

            return View("Preview", viewModel);
        }
       

        // GET: Clients
        public ViewResult Index()
        {
            var clients = _context.Clients.ToList();

            return View(clients);
        }

        public ActionResult Edit(int id)
        {
            var emailAddressContext = _context.EmailAddresses;
            List<EmailAddress> clientDistribution = new List<EmailAddress>();

            foreach (var email in emailAddressContext)
            {
                if (email.ClientId == id) clientDistribution.Add(email);
            }

            var viewModel = new ClientViewModel
            {
                Client = _context.Clients.SingleOrDefault(c => c.Id == id),
                EmailAddresses = clientDistribution

            };
    

            return View("ClientForm", viewModel);
        }
    }
}