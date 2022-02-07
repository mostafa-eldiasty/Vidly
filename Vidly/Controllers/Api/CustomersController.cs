using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // Get/Api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            return Ok(_context.Customers
                .Include(c=>c.MembershipType)
                .Where(c=>c.Name.Contains(query) || string.IsNullOrEmpty(query))
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>));
        }

        // Get/Api/Customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var Customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (Customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(Customer));
        }

        // Post/Api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }

        // Put/Api/Customers/id
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                NotFound();

            Mapper.CreateMap<CustomerDto, Customer>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.Map(customerDto, CustomerInDb);

            _context.SaveChanges();
        }

        // Delete/Api/Customers/id
        [HttpDelete]
        public void Delete(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                NotFound();

            _context.Customers.Remove(CustomerInDb);
            _context.SaveChanges();
        }

    }
}
