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
    public class TblComplaintsController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblComplaintsController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblComplaints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblComplaints>>> GetTblComplaints()
        {
            return await _context.Tbl_Complaints.ToListAsync();
        }

        [HttpGet("MaxIdComplain")]
        public int GetMaxIdCoplin()
        {
            int zmId = _context.Tbl_Complaints.OrderByDescending(p => p.ComplaintId).FirstOrDefault().ComplaintId;
            return zmId + 1;
        }


        // get list base on date 
        [HttpGet("GetComByDate/startDate={startDate}&endDate={endDate}")]


        public async Task<ActionResult<IEnumerable<TblComplaints>>> GetComByDate( DateTime startDate, DateTime endDate) 
        {
        return await _context.Tbl_Complaints.Include(r => r.ReasonCd).Where(c => c.DeliveryDate >= startDate & c.DeliveryDate <= endDate).ToListAsync();
    }



        // GET: api/TblComplaints/5

        [HttpGet("{id}")]
        public async Task<ActionResult<TblComplaints>> GetTblComplaints(int id)
        {
            var tblComplaints = await _context.Tbl_Complaints.Include(c => c.Cust).Where(d => d.ComplaintId == id).FirstOrDefaultAsync();

            if (tblComplaints == null)
            {
                return NotFound();
            }

            return tblComplaints;
        }

        // PUT: api/TblComplaints/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblComplaints(int id, TblComplaints tblComplaints)
        {
            if (id != tblComplaints.ComplaintId)
            {
                return BadRequest();
            }

            _context.Entry(tblComplaints).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblComplaintsExists(id))
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

        // POST: api/TblComplaints
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblComplaints>> PostTblComplaints(TblComplaints tblComplaints)
        {
            _context.Tbl_Complaints.Add(tblComplaints);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblComplaints", new { id = tblComplaints.ComplaintId }, tblComplaints);
           // return CreatedAtAction("IDPORT ", new { id = tblComplaints.ComplaintId });
        }

        // DELETE: api/TblComplaints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblComplaints>> DeleteTblComplaints(int id)
        {
            var tblComplaints = await _context.Tbl_Complaints.FindAsync(id);
            if (tblComplaints == null)
            {
                return NotFound();
            }

            _context.Tbl_Complaints.Remove(tblComplaints);
            await _context.SaveChangesAsync();

            return tblComplaints;
        }

        private bool TblComplaintsExists(int id)
        {
            return _context.Tbl_Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
