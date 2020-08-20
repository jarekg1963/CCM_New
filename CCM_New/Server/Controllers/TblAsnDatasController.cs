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
    public class TblAsnDatasController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblAsnDatasController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblAsnDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAsnData>>> GetTblAsnData()
        {
            return await _context.TblAsnData.ToListAsync();
        }

        //[HttpGet("GetByDeliveries/{sDn}")]
        [HttpGet("GetTblAsnDatabyAsnNr/{id}")]
        public async Task<ActionResult<IEnumerable<TblAsnData>>> GetTblAsnDatabyAsnNr(string id)
        {
            var tblAsnData = await _context.TblAsnData.Where(a => a.Asn == id).ToListAsync();

            if (tblAsnData == null)
            {
                return NotFound();
            }

            return tblAsnData;
        }

        // GET: api/TblAsnDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAsnData>> GetTblAsnData(string id)
        {
            var tblAsnData = await _context.TblAsnData.FindAsync(id);

            if (tblAsnData == null)
            {
                return NotFound();
            }

            return tblAsnData;
        }

        // PUT: api/TblAsnDatas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAsnData(string id, TblAsnData tblAsnData)
        {
            if (id != tblAsnData.Asn)
            {
                return BadRequest();
            }

            _context.Entry(tblAsnData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAsnDataExists(id))
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

        // POST: api/TblAsnDatas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblAsnData>> PostTblAsnData(TblAsnData tblAsnData)
        {
            _context.TblAsnData.Add(tblAsnData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblAsnDataExists(tblAsnData.Asn))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblAsnData", new { id = tblAsnData.Asn }, tblAsnData);
        }

        // DELETE: api/TblAsnDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblAsnData>> DeleteTblAsnData(string id)
        {
            var tblAsnData = await _context.TblAsnData.FindAsync(id);
            if (tblAsnData == null)
            {
                return NotFound();
            }

            _context.TblAsnData.Remove(tblAsnData);
            await _context.SaveChangesAsync();

            return tblAsnData;
        }

        private bool TblAsnDataExists(string id)
        {
            return _context.TblAsnData.Any(e => e.Asn == id);
        }
    }
}
