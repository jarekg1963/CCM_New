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
    public class TblDeliveriesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblDeliveriesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblDeliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeliveries>>> GetTblDeliveries()
        {
            return await _context.Tbl_Deliveries.ToListAsync();
        }


        // GET: api/TblDeliveries/5
        [HttpGet("GetByComplainId/{sCid}")]

        public async Task<ActionResult<IEnumerable<TblDeliveries>>> GetByDeliveries(int sCid)
        {
            var tblDeliveries = await _context.Tbl_Deliveries.Where(d => d.ComplaintId == sCid).ToListAsync();

            if (tblDeliveries == null)
            {
                return NotFound();
            }

            return tblDeliveries;
        }

        // GET: api/TblDeliveries/5
        [HttpGet("GetByDeliveries/{sDn}")]
       
        public async Task<ActionResult<IEnumerable<TblDeliveries>>> GetByDeliveries(string sDn)
        {
            var tblDeliveries = await _context.Tbl_Deliveries.Include(c => c.Comp)
                .Where(d => d.DeliveryNumber == sDn).ToListAsync();

            if (tblDeliveries == null)
            {
                return NotFound();
            }

            return tblDeliveries;
        }

        // GET: api/TblDeliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDeliveries>> GetTblDeliveries(int id)
        {
            var tblDeliveries = await _context.Tbl_Deliveries.FindAsync(id);

            if (tblDeliveries == null)
            {
                return NotFound();
            }

            return tblDeliveries;
        }

        // PUT: api/TblDeliveries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDeliveries(int id, TblDeliveries tblDeliveries)
        {
            if (id != tblDeliveries.DeliveryId)
            {
                return BadRequest();
            }

            _context.Entry(tblDeliveries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDeliveriesExists(id))
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

        // POST: api/TblDeliveries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblDeliveries>> PostTblDeliveries(TblDeliveries tblDeliveries)
        {
            _context.Tbl_Deliveries.Add(tblDeliveries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDeliveries", new { id = tblDeliveries.DeliveryId }, tblDeliveries);
        }

        [HttpDelete("DeleteTblDeliveriesIdComp/{id}")]
        public async Task<ActionResult<TblDeliveries>> DeleteTblDeliveriesIdComp(int id)
        {
            var tblDeliveries = await _context.Tbl_Deliveries.Where(d => d.ComplaintId == id).FirstOrDefaultAsync();
            if (tblDeliveries == null)
            {
                return NotFound();
            }

            _context.Tbl_Deliveries.Remove(tblDeliveries);
            await _context.SaveChangesAsync();

            return tblDeliveries;
        }


        // DELETE: api/TblDeliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblDeliveries>> DeleteTblDeliveries(int id)
        {
            var tblDeliveries = await _context.Tbl_Deliveries.FindAsync(id);
            if (tblDeliveries == null)
            {
                return NotFound();
            }

            _context.Tbl_Deliveries.Remove(tblDeliveries);
            await _context.SaveChangesAsync();

            return tblDeliveries;
        }

        private bool TblDeliveriesExists(int id)
        {
            return _context.Tbl_Deliveries.Any(e => e.DeliveryId == id);
        }
    }
}
