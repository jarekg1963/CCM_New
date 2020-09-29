using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCM_New.Server.Data;
using CCM_New.Shared;

namespace CCM_New.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblCustomersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblCustomersController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblCustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustomers>>> GetTblCustomers()
        {
            return await _context.TblCustomers.ToListAsync();
        }

        // GET: api/TblCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustomers>> GetTblCustomers(string id)
        {
            var tblCustomers = await _context.TblCustomers.FindAsync(id);

            if (tblCustomers == null)
            {
                return NotFound();
            }

            return tblCustomers;
        }

        // PUT: api/TblCustomers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCustomers(string id, TblCustomers tblCustomers)
        {
            if (id != tblCustomers.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(tblCustomers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCustomersExists(id))
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

        // POST: api/TblCustomers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblCustomers>> PostTblCustomers(TblCustomers tblCustomers)
        {
            _context.TblCustomers.Add(tblCustomers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblCustomersExists(tblCustomers.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblCustomers", new { id = tblCustomers.CustomerId }, tblCustomers);
        }

        // DELETE: api/TblCustomers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblCustomers>> DeleteTblCustomers(string id)
        {
            var tblCustomers = await _context.TblCustomers.FindAsync(id);
            if (tblCustomers == null)
            {
                return NotFound();
            }

            _context.TblCustomers.Remove(tblCustomers);
            await _context.SaveChangesAsync();

            return tblCustomers;
        }

        private bool TblCustomersExists(string id)
        {
            return _context.TblCustomers.Any(e => e.CustomerId == id);
        }
    }
}
