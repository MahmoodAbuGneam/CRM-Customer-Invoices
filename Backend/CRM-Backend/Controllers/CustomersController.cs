using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiTech.Models;
using PractiTech.requests;

namespace PractiTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerRequest customer)
        {

            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Name == customer.Name);
            if (existingCustomer != null)
            {
                return Conflict(new { message = "Customer name already exists" });

            }


            var newCustomer= new Customer() { ID=Guid.NewGuid(),Name=customer.Name, Address = customer.Address, PhoneNum = customer.PhoneNum};
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = newCustomer.ID }, newCustomer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Customers
        [HttpDelete]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            _context.Customers.RemoveRange(_context.Customers);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
