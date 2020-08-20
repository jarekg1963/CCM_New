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
    public class TblDeliveryLinesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblDeliveryLinesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblDeliveryLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeliveryLines>>> GetTblDeliveryLines()
        {
            return await _context.TblDeliveryLines.ToListAsync();
        }

        [HttpGet("GetTblDeliveryLinesByCid/{id}")]
        public async Task<ActionResult<IEnumerable<TblDeliveryLines>>> GetTblDeliveryLinesByCid(int id)
        {
            var tblDeliveryLines = await _context.TblDeliveryLines.Where(c => c.ComplaintId == id).ToListAsync();

            if (tblDeliveryLines == null)
            {
                return NotFound();
            }

            return tblDeliveryLines;
        }

        // GET: api/TblDeliveryLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDeliveryLines>> GetTblDeliveryLines(int id)
        {
            var tblDeliveryLines = await _context.TblDeliveryLines.FindAsync(id);

            if (tblDeliveryLines == null)
            {
                return NotFound();
            }

            return tblDeliveryLines;
        }

        // PUT: api/TblDeliveryLines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDeliveryLines(int id, TblDeliveryLines tblDeliveryLines)
        {
            if (id != tblDeliveryLines.DeliveryLineId)
            {
                return BadRequest();
            }

            _context.Entry(tblDeliveryLines).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDeliveryLinesExists(id))
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

        // POST: api/TblDeliveryLines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblDeliveryLines>> PostTblDeliveryLines(TblDeliveryLines tblDeliveryLines)
        {
            _context.TblDeliveryLines.Add(tblDeliveryLines);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDeliveryLines", new { id = tblDeliveryLines.DeliveryLineId }, tblDeliveryLines);
        }

        // DELETE: api/TblDeliveryLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblDeliveryLines>> DeleteTblDeliveryLines(int id)
        {
            var tblDeliveryLines = await _context.TblDeliveryLines.FindAsync(id);
            if (tblDeliveryLines == null)
            {
                return NotFound();
            }

            _context.TblDeliveryLines.Remove(tblDeliveryLines);
            await _context.SaveChangesAsync();

            return tblDeliveryLines;
        }

        private bool TblDeliveryLinesExists(int id)
        {
            return _context.TblDeliveryLines.Any(e => e.DeliveryLineId == id);
        }
    }
}
