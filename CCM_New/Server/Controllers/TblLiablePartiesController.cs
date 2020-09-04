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
    public class TblLiablePartiesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblLiablePartiesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblLiableParties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblLiableParties>>> GetTblLiableParties()
        {
            return await _context.TblLiableParties.ToListAsync();
        }

        // GET: api/TblLiableParties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblLiableParties>> GetTblLiableParties(int id)
        {
            var tblLiableParties = await _context.TblLiableParties.FindAsync(id);

            if (tblLiableParties == null)
            {
                return NotFound();
            }

            return tblLiableParties;
        }

        // PUT: api/TblLiableParties/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblLiableParties(int id, TblLiableParties tblLiableParties)
        {
            if (id != tblLiableParties.LiablePartyId)
            {
                return BadRequest();
            }

            _context.Entry(tblLiableParties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblLiablePartiesExists(id))
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

        // POST: api/TblLiableParties
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblLiableParties>> PostTblLiableParties(TblLiableParties tblLiableParties)
        {
            _context.TblLiableParties.Add(tblLiableParties);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblLiableParties", new { id = tblLiableParties.LiablePartyId }, tblLiableParties);
        }

        // DELETE: api/TblLiableParties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblLiableParties>> DeleteTblLiableParties(int id)
        {
            var tblLiableParties = await _context.TblLiableParties.FindAsync(id);
            if (tblLiableParties == null)
            {
                return NotFound();
            }

            _context.TblLiableParties.Remove(tblLiableParties);
            await _context.SaveChangesAsync();

            return tblLiableParties;
        }

        private bool TblLiablePartiesExists(int id)
        {
            return _context.TblLiableParties.Any(e => e.LiablePartyId == id);
        }
    }
}
