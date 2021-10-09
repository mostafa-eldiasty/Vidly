using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _Context;

        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        
        public ActionResult Index()
        {
            var custopmers = _Context.Customers.Include(x => x.MembershipType).ToList();
            return View(custopmers);
        }

        public ActionResult Details(int id)
        {
            var customer = _Context.Customers.Include(x=>x.MembershipType).SingleOrDefault(x => x.Id == id);
           if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}