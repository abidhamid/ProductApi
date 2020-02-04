using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;

            if (_context.ProductItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.ProductItems.Add(new ProductItem { Name = "Nvidia 1080", Category = "Graphics card", Description = "New", Price = 300 });
                _context.ProductItems.Add(new ProductItem { Name = "Nvidia 660", Category = "Graphics card", Description = "Old", Price = 110 });
                _context.ProductItems.Add(new ProductItem { Name = "Nvidia 1060", Category = "Graphics card", Description = "New", Price = 180 });
                _context.SaveChanges();
            }
        }


        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProductItems()
        {
            return await _context.ProductItems.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(long id)
        {
            var productItem = await _context.ProductItems.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            return productItem;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductItem>> PostProductItem(ProductItem item)
        {
            _context.ProductItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductItem), new { id = item.Id }, item);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductItem(long id, ProductItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem(long id)
        {
            var productItem = await _context.ProductItems.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            _context.ProductItems.Remove(productItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
