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
    public class TblTaskUsersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblTaskUsersController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblTaskUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTaskUser>>> GetTblTaskUser()
        {
            return await _context.TblTaskUser.ToListAsync();
        }

        [HttpGet("GetTblFullTaskUser/{id}")]
        public async Task<ActionResult<IEnumerable<TblTaskUser>>> GetTblFullTaskUser(int id)
        {
            return await _context.TblTaskUser.Where(u => u.UserId == id).Include(d => d.Ur).Include(e => e.Ta).ToListAsync();
        }

        // GET: api/TblTaskUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTaskUser>> GetTblTaskUser(int id)
        {
            var tblTaskUser = await _context.TblTaskUser.FindAsync(id);

            if (tblTaskUser == null)
            {
                return NotFound();
            }

            return tblTaskUser;
        }

        // PUT: api/TblTaskUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTaskUser(int id, TblTaskUser tblTaskUser)
        {
            if (id != tblTaskUser.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(tblTaskUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblTaskUserExists(id))
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

        // POST: api/TblTaskUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblTaskUser>> PostTblTaskUser(TblTaskUser tblTaskUser)
        {
            _context.TblTaskUser.Add(tblTaskUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblTaskUserExists(tblTaskUser.TaskId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblTaskUser", new { id = tblTaskUser.TaskId }, tblTaskUser);
        }

        // DELETE: api/TblTaskUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblTaskUser>> DeleteTblTaskUser(int id)
        {
            var tblTaskUser = await _context.TblTaskUser.FindAsync(id);
            if (tblTaskUser == null)
            {
                return NotFound();
            }

            _context.TblTaskUser.Remove(tblTaskUser);
            await _context.SaveChangesAsync();

            return tblTaskUser;
        }

        private bool TblTaskUserExists(int id)
        {
            return _context.TblTaskUser.Any(e => e.TaskId == id);
        }
    }
}
