using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore;
using PractiTech.Models;
using PractiTech.requests;
namespace PractiTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemsController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }


        



        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemRequest item)
        {
            var newItem = new Item() { id = Guid.NewGuid(), name = item.name , invoice_id = item.invoice_id , price = item.price , quantity = item.quantity};
            
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = newItem.id }, newItem);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET : api/Items/Invoice_id

        [HttpGet("Invoice/{invoice_id}")]

        public async Task<ActionResult<Invoice>> GetItemsByInvoiceId(Guid invoice_id)
        {
            var invoice_ids = await _context.Items.Where(Item => Item.invoice_id == invoice_id).ToListAsync();

            if (invoice_ids == null)
            {
                return NotFound();
            }

            return Ok(invoice_ids);
        }


        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.id == id);
        }
    }
}
