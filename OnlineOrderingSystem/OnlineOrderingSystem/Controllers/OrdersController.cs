using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrderingSystem.Models;

namespace OnlineOrderingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDBContext _context;

        public OrdersController(OrdersDBContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrderTbl()
        {
            if (_context.OrderTbl == null)
            {
                return NotFound();
            }
            return await _context.OrderTbl.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            if (_context.OrderTbl == null)
            {
                return NotFound();
            }
            var orderTbl = await _context.OrderTbl.FindAsync(id);

            if (orderTbl == null)
            {
                return NotFound();
            }

            return orderTbl;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, OrderDTO dto)
        {
            var orderTbl = await _context.OrderTbl.FindAsync(id);

            if (id != orderTbl.ItemCode)
            {
                return BadRequest();
            }

            _context.Entry(orderTbl).State = EntityState.Modified;




            orderTbl.OrderDelivery = dto.OrderDelivery;
            orderTbl.OrderAddress = dto.OrderAddress;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            if (_context.OrderTbl == null)
            {
                return Problem("Entity set 'OrderSystemDBContext.OrderTbls'  is null.");
            }
            _context.OrderTbl.Add(orders);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrdersExists(orders.ItemCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderTbl", new { id = orders.ItemCode }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            if (_context.OrderTbl == null)
            {
                return NotFound();
            }
            var orderTbl = await _context.OrderTbl.FindAsync(id);
            if (orderTbl == null)
            {
                return NotFound();
            }

            _context.OrderTbl.Remove(orderTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersExists(int id)
        {
            return (_context.OrderTbl?.Any(e => e.ItemCode == id)).GetValueOrDefault();
        }

    }
}
