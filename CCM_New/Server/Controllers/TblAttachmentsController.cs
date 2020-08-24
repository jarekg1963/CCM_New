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
    public class TblAttachmentsController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblAttachmentsController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblAttachments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAttachments>>> GetTblAttachments()
        {
            return await _context.TblAttachments.ToListAsync();
        }


        [HttpGet("GetTblAttachmentsbyId/{id}")]
        public async Task<ActionResult<IEnumerable<TblAttachments>>> GetTblAttachmentsbyId(int id)
        {
            var tblAttachments = await _context.TblAttachments.Where(c => c.ComplaintId == id).ToListAsync(); ;

            if (tblAttachments == null)
            {
                return NotFound();
            }

            return tblAttachments;
        }


        // GET: api/TblAttachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAttachments>> GetTblAttachments(int id)
        {
            var tblAttachments = await _context.TblAttachments.FindAsync(id);

            if (tblAttachments == null)
            {
                return NotFound();
            }

            return tblAttachments;
        }

        // PUT: api/TblAttachments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAttachments(int id, TblAttachments tblAttachments)
        {
            if (id != tblAttachments.UploadId)
            {
                return BadRequest();
            }

            _context.Entry(tblAttachments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAttachmentsExists(id))
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

        // POST: api/TblAttachments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblAttachments>> PostTblAttachments(TblAttachments tblAttachments)
        {
            _context.TblAttachments.Add(tblAttachments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblAttachments", new { id = tblAttachments.UploadId }, tblAttachments);
        }

        // DELETE: api/TblAttachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblAttachments>> DeleteTblAttachments(int id)
        {
            var tblAttachments = await _context.TblAttachments.FindAsync(id);
            if (tblAttachments == null)
            {
                return NotFound();
            }

            _context.TblAttachments.Remove(tblAttachments);
            await _context.SaveChangesAsync();

            return tblAttachments;
        }

        private bool TblAttachmentsExists(int id)
        {
            return _context.TblAttachments.Any(e => e.UploadId == id);
        }
    }
}
