using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var ViewModel = new CustomerFormViewModel
                {
                    MembershipTypes = _Context.MembershipType.ToList(),
                    Customer = customer
                };

                return View("CustomerForm", ViewModel);
            }

            if(customer.Id == 0)
                _Context.Customers.Add(customer);
            else
            {
                Customer customerInDB = _Context.Customers.Single(x => x.Id == customer.Id);
                customerInDB.Name = customer.Name;
                customerInDB.Birthdate = customer.Birthdate;
                customerInDB.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDB.IsSubscibedToNewsletter = customer.IsSubscibedToNewsletter;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        public ActionResult New()
        {
            var MembershipTypes = _Context.MembershipType.ToList();
            var NewCustomerViewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = MembershipTypes
            };
            return View("CustomerForm",NewCustomerViewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = _Context.Customers.Include(x=>x.MembershipType).SingleOrDefault(x => x.Id == id);
           if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _Context.Customers.Where(x => x.Id == id).SingleOrDefault();
            if (customer == null)
                return HttpNotFound();
            var ViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _Context.MembershipType.ToList()
            };
            return View("CustomerForm", ViewModel);
        }
    }
}