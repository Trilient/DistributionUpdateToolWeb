using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistributionUpdateToolWeb.Models;

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

        public ActionResult Index(int emailId)
        {
            return View(GetEmailAddressFromDatabase(emailId));
        }

        public ActionResult Edit(int emailId)
        {
            return View(GetEmailAddressFromDatabase(emailId));
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