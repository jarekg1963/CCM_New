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
    public class TblRootCausesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblRootCausesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblRootCauses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblRootCauses>>> GetTblRootCauses()
        {
            return await _context.TblRootCauses.ToListAsync();
        }

        // GET: api/TblRootCauses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblRootCauses>> GetTblRootCauses(int id)
        {
            var tblRootCauses = await _context.TblRootCauses.FindAsync(id);

            if (tblRootCauses == null)
            {
                return NotFound();
            }

            return tblRootCauses;
        }

        // PUT: api/TblRootCauses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblRootCauses(int id, TblRootCauses tblRootCauses)
        {
            if (id != tblRootCauses.RootCauseId)
            {
                return BadRequest();
            }

            _context.Entry(tblRootCauses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblRootCausesExists(id))
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

        // POST: api/TblRootCauses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblRootCauses>> PostTblRootCauses(TblRootCauses tblRootCauses)
        {
            _context.TblRootCauses.Add(tblRootCauses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblRootCauses", new { id = tblRootCauses.RootCauseId }, tblRootCauses);
        }

        // DELETE: api/TblRootCauses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblRootCauses>> DeleteTblRootCauses(int id)
        {
            var tblRootCauses = await _context.TblRootCauses.FindAsync(id);
            if (tblRootCauses == null)
            {
                return NotFound();
            }

            _context.TblRootCauses.Remove(tblRootCauses);
            await _context.SaveChangesAsync();

            return tblRootCauses;
        }

        private bool TblRootCausesExists(int id)
        {
            return _context.TblRootCauses.Any(e => e.RootCauseId == id);
        }
    }
}
