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
                // Receive data from form submission and create a list of EmailAddress objects to populate
                string textDistro = collection["TextEmailDistro"];
                string[] emails = textDistro.Split(';');

                foreach (var address in emails)
                {
                    EmailAddress email = new EmailAddress { Email = address };
                    emailDistro.Add(email);
                }

                client.EmailAddresses = emailDistro;
                _context.Clients.Add(client);
                _context.SaveChanges();
            }
            else if (client.Id > 0)
            {
                var clientInDb = GetClientFromDb(client.Id);

                clientInDb.Name = client.Name;

                _context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return View("Details", client);
        }
       

        // GET: Clients
        public ViewResult Index()
        {
            var clients = _context.Clients.ToList();

            return View(clients);
        }

        public ActionResult Details(FormCollection collection, int id, int emailId, bool edit)
        {
            if (edit)
            {
                string editEmail = collection["EditEmailAddress"];
                EmailAddress emailInDb = _context.EmailAddresses.Single(e => e.Id == emailId);

                emailInDb.Email = editEmail;

                _context.SaveChanges();
            }

            return View(GetClientFromDb(id));
        }

        public ActionResult Edit(int id)
        {
            return View("ClientForm", GetClientFromDb(id));
        }

        public ActionResult AddEmailAddress(FormCollection collection, int clientId)
        {
            string newEmail = collection["NewEmailAddress"];
            _context.EmailAddresses.Add(new EmailAddress { ClientId = clientId, Email = newEmail });
            _context.SaveChanges();

            return View("Details", GetClientFromDb(clientId));
        }

        public ActionResult EditEmailAddress(int emailId, int clientId)
        {
            ViewBag.EditEmailId = emailId;

            return View("Details", GetClientFromDb(clientId));
        }

        public ActionResult DeleteEmailAddress(int id, int clientId)
        {
            EmailAddress emailAddress = _context.EmailAddresses.Single(e => e.Id == id);
            _context.EmailAddresses.Remove(emailAddress);
            _context.SaveChanges();

            return View("Details", GetClientFromDb(clientId));
        }

        //---------------------------------------------------------------------------------------

        private Client GetClientFromDb(int id)
        {
            return _context.Clients.SingleOrDefault(c => c.Id == id);
        }
    }
}