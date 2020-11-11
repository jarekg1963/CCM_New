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
    public class TblSoUsersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblSoUsersController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblSoUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSoUser>>> GetTblSoUser()
        {
            return await _context.TblSoUser.ToListAsync();
        }


        [HttpGet("GetTblSoUserByUser/{id}")]
        public async Task<ActionResult<IEnumerable<TblSoUser>>> GetTblSoUserByUser(int id)
        {
            return await _context.TblSoUser.Where(c => c.UserId == id).Include(d => d.Sorg).ToListAsync();
        }

        // GET: api/TblSoUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSoUser>> GetTblSoUser(int id)
        {
            var tblSoUser = await _context.TblSoUser.FindAsync(id);

            if (tblSoUser == null)
            {
                return NotFound();
            }

            return tblSoUser;
        }

        // PUT: api/TblSoUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSoUser(int id, TblSoUser tblSoUser)
        {
            if (id != tblSoUser.So)
            {
                return BadRequest();
            }

            _context.Entry(tblSoUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSoUserExists(id))
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

        // POST: api/TblSoUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblSoUser>> PostTblSoUser(TblSoUser tblSoUser)
        {
            _context.TblSoUser.Add(tblSoUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblSoUserExists(tblSoUser.So))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblSoUser", new { id = tblSoUser.So }, tblSoUser);
        }

        // DELETE: api/TblSoUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblSoUser>> DeleteTblSoUser(int id)
        {
            var tblSoUser = await _context.TblSoUser.FindAsync(id);
            if (tblSoUser == null)
            {
                return NotFound();
            }

            _context.TblSoUser.Remove(tblSoUser);
            await _context.SaveChangesAsync();

            return tblSoUser;
        }

        private bool TblSoUserExists(int id)
        {
            return _context.TblSoUser.Any(e => e.So == id);
        }
    }
}
