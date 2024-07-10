using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiTech.Models;
using System.Collections.Generic;
using PractiTech.requests;

namespace PractiTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoicesController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }


        // GET: api/Invoices/customer/5
        // Get by customer id
        [HttpGet("Customer/{customer_id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByCustomerId(Guid customer_id)
        {
            var customer_ids = await _context.Invoices.Where(Invoice => Invoice.customer_id == customer_id).ToListAsync();

            if (customer_ids == null)
            {
                return NotFound();
            }

            return Ok(customer_ids);
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceRequest invoice)
        {
            var newInvoice = new Invoice() { id = Guid.NewGuid() , customer_id = invoice.customer_id , Date = invoice.Date };

            _context.Invoices.Add(newInvoice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(newInvoice.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoice", new { id = newInvoice.id }, newInvoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            try
            {
               await _context.SaveChangesAsync();
            }

            catch (Exception ex) 
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoices.Any(e => e.id == id);
        }
    }
}
