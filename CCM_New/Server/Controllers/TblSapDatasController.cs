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
    public class TblSapDatasController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblSapDatasController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblSapDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSapData>>> GetTblSapData()
        {
            return await _context.TblSapData.ToListAsync();
        }

        [HttpGet("GetByDn/{sDn}")]
        public async Task<ActionResult<IEnumerable<TblSapData>>> GetByDn(string sDn)
        {
            return await _context.TblSapData.Where( c => c.Vbeln == sDn).ToListAsync();
        }

        // GET: api/TblSapDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSapData>> GetTblSapData(string id)
        {
            var tblSapData = await _context.TblSapData.FindAsync(id);

            if (tblSapData == null)
            {
                return NotFound();
            }

            return tblSapData;
        }

        // PUT: api/TblSapDatas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSapData(string id, TblSapData tblSapData)
        {
            if (id != tblSapData.Vbeln)
            {
                return BadRequest();
            }

            _context.Entry(tblSapData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSapDataExists(id))
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

        // POST: api/TblSapDatas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblSapData>> PostTblSapData(TblSapData tblSapData)
        {
            _context.TblSapData.Add(tblSapData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblSapDataExists(tblSapData.Vbeln))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblSapData", new { id = tblSapData.Vbeln }, tblSapData);
        }

        // DELETE: api/TblSapDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblSapData>> DeleteTblSapData(string id)
        {
            var tblSapData = await _context.TblSapData.FindAsync(id);
            if (tblSapData == null)
            {
                return NotFound();
            }

            _context.TblSapData.Remove(tblSapData);
            await _context.SaveChangesAsync();

            return tblSapData;
        }

        private bool TblSapDataExists(string id)
        {
            return _context.TblSapData.Any(e => e.Vbeln == id);
        }
    }
}
