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
    public class TblDcUsersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblDcUsersController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblDcUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDcUser>>> GetTblDcUser()
        {
            return await _context.TblDcUser.ToListAsync();
        }

        // GET: api/TblDcUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDcUser>> GetTblDcUser(int id)
        {
            var tblDcUser = await _context.TblDcUser.FindAsync(id);

            if (tblDcUser == null)
            {
                return NotFound();
            }

            return tblDcUser;
        }

        [HttpGet("GetTblDcUserByUser/{id}")]
        public async Task<ActionResult<IEnumerable<TblDcUser>>> GetTblDcUserByUser(int id)
        {
            return await _context.TblDcUser.Where(c => c.UserId == id).Include(d => d.DC).ToListAsync();
        }


        [HttpGet("GetTblDcIfExist/idPrac={id} && Dcid = {idDC}")]
        public ActionResult<bool> GetTblDcName(int id, int idDC)
        {

            var tblDc = _context.TblDcUser.Any(e => e.DcId == idDC && e.UserId == id);

            if (tblDc == true)
            {
                return true;
            }

            return false;
        }

        // PUT: api/TblDcUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDcUser(int id, TblDcUser tblDcUser)
        {
            if (id != tblDcUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblDcUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDcUserExists(id))
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

        // POST: api/TblDcUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblDcUser>> PostTblDcUser(TblDcUser tblDcUser)
        {
            _context.TblDcUser.Add(tblDcUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDcUser", new { id = tblDcUser.Id }, tblDcUser);
        }

        // DELETE: api/TblDcUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblDcUser>> DeleteTblDcUser(int id)
        {
            var tblDcUser = await _context.TblDcUser.FindAsync(id);
            if (tblDcUser == null)
            {
                return NotFound();
            }

            _context.TblDcUser.Remove(tblDcUser);
            await _context.SaveChangesAsync();

            return tblDcUser;
        }

        private bool TblDcUserExists(int id)
        {
            return _context.TblDcUser.Any(e => e.Id == id);
        }
    }
}
