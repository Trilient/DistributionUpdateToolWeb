using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistributionUpdateToolWeb.Models;
using DistributionUpdateToolWeb.ViewModels;

namespace DistributionUpdateToolWeb.Controllers
{
    public class EmailController : Controller
    {
        ApplicationDbContext _context;

        public EmailController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Edit(int emailId)
        {
            return View(GetEmailAddressFromDatabase(emailId));
        }

        public ActionResult Delete(int emailId, int clientId)
        {
            var emailToDelete = GetEmailAddressFromDatabase(emailId);
            var returnToClient = _context.Clients.SingleOrDefault(c => c.Id == clientId);
            _context.EmailAddresses.Remove(emailToDelete);

            _context.SaveChanges();

            return RedirectToAction("Details", "Clients", returnToClient);
        }

        public ActionResult GetDelete(int emailId, int clientId)
        {
            ClientSingleViewModel viewModel = new ClientSingleViewModel
            { 
                Client = _context.Clients.SingleOrDefault(c => c.Id == clientId),
                EmailAddress = _context.EmailAddresses.SingleOrDefault(e => e.Id == emailId)

            };

            return View("Delete", viewModel);
        }

        [HttpPost]
        public ActionResult Save(EmailAddress emailAddress, bool details)
        {
            var emailInDb = _context.EmailAddresses.Single(e => e.Id == emailAddress.Id);

            emailInDb.CustomerName = emailAddress.CustomerName;
            emailInDb.Email = emailAddress.Email;
            _context.SaveChanges();

            if (details)
                return RedirectToAction("Details", "Clients", emailAddress);
            else
                return View("Index", emailAddress);
        }

        private EmailAddress GetEmailAddressFromDatabase(int id)
        {
            return _context.EmailAddresses.Single(e => e.Id == id);
        }
    }
}