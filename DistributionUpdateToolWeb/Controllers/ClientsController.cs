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
        public ActionResult Save(Client client, FormCollection collection)
        {
            List<EmailAddress> emailDistro = new List<EmailAddress>();

            // Client ID will be 0 by default if it is a new client
            if (client.Id == 0)
            {
                // Add new client to DB and save it after, this is so we can pull the new client from the DB and assign the new email address's to the ID in the database
                // Otherwise the client ID will be 0
                client = _context.Clients.Add(client);
                _context.SaveChanges();

                // Receive data from form submission and create a list of EmailAddress objects to populate
                string textDistro = collection["TextEmailDistro"];
                string[] emails = textDistro.Split(';');

                foreach (var address in emails)
                {
                    EmailAddress email = new EmailAddress { ClientId = client.Id, Email = address };
                    _context.EmailAddresses.Add(email);
                    emailDistro.Add(email);
                }

                _context.SaveChanges();
            }
            else if (client.Id > 0)
            {
                var clientInDb = GetClientFromDb(client.Id);
                emailDistro = GetEmailAddresses(client.Id);

                clientInDb.Name = client.Name;

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

            return View("Details", viewModel);
        }
       

        // GET: Clients
        public ViewResult Index()
        {
            var clients = _context.Clients.ToList();

            return View(clients);
        }

        public ActionResult Details(int id)
        {
            var viewModel = new ClientViewModel
            {
                Client = GetClientFromDb(id),
                EmailAddresses = GetEmailAddresses(id)

            };
    
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = new ClientViewModel
            {
                Client = GetClientFromDb(id),
                EmailAddresses = GetEmailAddresses(id)

            };

            return View("ClientForm", viewModel);
        }

        public ActionResult EditEmailAddress(EmailAddress emailAddress)
        {
            Client client = GetClientFromDb(emailAddress.ClientId);
            var emailInDb = _context.EmailAddresses.Single(e => e.Id == emailAddress.Id);

            return View("Details", client);
        }

        //---------------------------------------------------------------------------------------

        private Client GetClientFromDb(int id)
        {
            return _context.Clients.SingleOrDefault(c => c.Id == id);
        }

        private List<EmailAddress> GetEmailAddresses(int clientId)
        {
            List<EmailAddress> clientDistributionList = new List<EmailAddress>();

            foreach (var email in _context.EmailAddresses)
            {
                if (email.ClientId == clientId) clientDistributionList.Add(email);
            }

            return clientDistributionList;
        }
    }
}