using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCM_New.Server.Data;
using CCM_New.Shared;
using Syncfusion.Blazor.Schedule;

namespace CCM_New.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblReasoncodesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblReasoncodesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblReasoncodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblReasoncodes>>> GetTblReasoncodes()
        {
            return await _context.TblReasoncodes.ToListAsync();
        }

        [HttpGet]
        [Route("GetTblReasoncodesType")]
        public IQueryable<ReasonCodeExtended> GetTblReasoncodesType()
        {

            return _context.TblReasoncodes.Include(s => s.ClaimType).Select(p =>
          new ReasonCodeExtended
          {
              ReasoncodeId = p.ReasoncodeId,
              ReasoncodeName = p.ClaimType.ComplaintTypeName.Substring(0, 4) + " " +p.ReasoncodeName ,
          
          });

        }
        // GET: api/TblReasoncodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblReasoncodes>> GetTblReasoncodes(int id)
        {
            var tblReasoncodes = await _context.TblReasoncodes.FindAsync(id);

            if (tblReasoncodes == null)
            {
                return NotFound();
            }

            return tblReasoncodes;
        }

        // PUT: api/TblReasoncodes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblReasoncodes(int id, TblReasoncodes tblReasoncodes)
        {
            if (id != tblReasoncodes.ReasoncodeId)
            {
                return BadRequest();
            }

            _context.Entry(tblReasoncodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblReasoncodesExists(id))
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

        // POST: api/TblReasoncodes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblReasoncodes>> PostTblReasoncodes(TblReasoncodes tblReasoncodes)
        {
            _context.TblReasoncodes.Add(tblReasoncodes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblReasoncodesExists(tblReasoncodes.ReasoncodeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblReasoncodes", new { id = tblReasoncodes.ReasoncodeId }, tblReasoncodes);
        }

        // DELETE: api/TblReasoncodes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblReasoncodes>> DeleteTblReasoncodes(int id)
        {
            var tblReasoncodes = await _context.TblReasoncodes.FindAsync(id);
            if (tblReasoncodes == null)
            {
                return NotFound();
            }

            _context.TblReasoncodes.Remove(tblReasoncodes);
            await _context.SaveChangesAsync();

            return tblReasoncodes;
        }
 public int IdNameReasonCode { get; set; }
        public string ReasonCode { get; set; }
        private bool TblReasoncodesExists(int id)
        {
            return _context.TblReasoncodes.Any(e => e.ReasoncodeId == id);
        }
    
    }
   
}
