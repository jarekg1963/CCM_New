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
    public class TblDcsController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblDcsController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblDcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDc>>> GetTblDc()
        {
            return await _context.TblDc.ToListAsync();
        }

        // GET: api/TblDcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDc>> GetTblDc(int id)
        {
            var tblDc = await _context.TblDc.FindAsync(id);

            if (tblDc == null)
            {
                return NotFound();
            }

            return tblDc;
        }

   

        // PUT: api/TblDcs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDc(int id, TblDc tblDc)
        {
            if (id != tblDc.DcId)
            {
                return BadRequest();
            }

            _context.Entry(tblDc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDcExists(id))
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

        // POST: api/TblDcs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblDc>> PostTblDc(TblDc tblDc)
        {
            _context.TblDc.Add(tblDc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDc", new { id = tblDc.DcId }, tblDc);
        }

        // DELETE: api/TblDcs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblDc>> DeleteTblDc(int id)
        {
            var tblDc = await _context.TblDc.FindAsync(id);
            if (tblDc == null)
            {
                return NotFound();
            }

            _context.TblDc.Remove(tblDc);
            await _context.SaveChangesAsync();

            return tblDc;
        }

        private bool TblDcExists(int id)
        {
            return _context.TblDc.Any(e => e.DcId == id);
        }
    }
}
