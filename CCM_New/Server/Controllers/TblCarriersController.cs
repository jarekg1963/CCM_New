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
    public class TblCarriersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblCarriersController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblCarriers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCarriers>>> GetTblCarriers()
        {
            return await _context.TblCarriers.ToListAsync();
        }

        // GET: api/TblCarriers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCarriers>> GetTblCarriers(string id)
        {
            var tblCarriers = await _context.TblCarriers.FindAsync(id);

            if (tblCarriers == null)
            {
                return NotFound();
            }

            return tblCarriers;
        }

        // PUT: api/TblCarriers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCarriers(string id, TblCarriers tblCarriers)
        {
            if (id != tblCarriers.CarrierId2)
            {
                return BadRequest();
            }

            _context.Entry(tblCarriers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCarriersExists(id))
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

        // POST: api/TblCarriers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblCarriers>> PostTblCarriers(TblCarriers tblCarriers)
        {
            _context.TblCarriers.Add(tblCarriers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblCarriersExists(tblCarriers.CarrierId2))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblCarriers", new { id = tblCarriers.CarrierId2 }, tblCarriers);
        }

        // DELETE: api/TblCarriers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblCarriers>> DeleteTblCarriers(string id)
        {
            var tblCarriers = await _context.TblCarriers.FindAsync(id);
            if (tblCarriers == null)
            {
                return NotFound();
            }

            _context.TblCarriers.Remove(tblCarriers);
            await _context.SaveChangesAsync();

            return tblCarriers;
        }

        private bool TblCarriersExists(string id)
        {
            return _context.TblCarriers.Any(e => e.CarrierId2 == id);
        }
    }
}
