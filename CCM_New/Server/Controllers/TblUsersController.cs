using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCM_New.Server.Data;
using CCM_New.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CCM_New.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUsersController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblUsersController(CCMContext context)
        {
            _context = context;
            Cache cache = Cache.instance();
        }

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsers>>> GetTblUsers()
        {
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsers>> GetTblUsers(int id)
        {
            var tblUsers = await _context.TblUsers.FindAsync(id);

            if (tblUsers == null)
            {
                return NotFound();
            }

            return tblUsers;
        }


        [HttpGet]
        [Route("FindUserByName/{zName}")]

        public async Task<ActionResult<IEnumerable<TblUsers>>> FindUserByName(string zName)
        {
            // var tblUsers = await _context.TblUsers.Where(c => c.UserName == zName).SingleOrDefaultAsync();
            // var tblUsers = await _context.TblUsers.FindAsync(zName);


            //if (tblUsers == null)
            //{
            //    return NotFound();
            //}

            return await _context.TblUsers.Where(c => c.UserName == zName).ToListAsync();
;        }


        // PUT: api/TblUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUsers(int id, TblUsers tblUsers)
        {
            if (id != tblUsers.Id)
            {
                return BadRequest();
            }

            tblUsers.UserPassword = Cache.CreateMD5(tblUsers.UserPassword);

            _context.Entry(tblUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUsersExists(id))
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


        // put user without encription 

        [HttpPut("PutTblUsersNoEncript/{id}")]
      //  [Route("PutTblUsersNoEncript")]
        public async Task<IActionResult> PutTblUsersNoEncript(int id, TblUsers tblUsers)
        {
            if (id != tblUsers.Id)
            {
                return BadRequest();
            }
            _context.Entry(tblUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUsersExists(id))
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

        // POST: api/TblUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblUsers>> PostTblUsers(TblUsers tblUsers)
        {
            tblUsers.UserPassword = Cache.CreateMD5(tblUsers.UserPassword);
            _context.TblUsers.Add(tblUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUsers", new { id = tblUsers.Id }, tblUsers);
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblUsers>> DeleteTblUsers(int id)
        {
            var tblUsers = await _context.TblUsers.FindAsync(id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUsers);
            await _context.SaveChangesAsync();

            return tblUsers;
        }

        private bool TblUsersExists(int id)
        {
            return _context.TblUsers.Any(e => e.Id == id);
        }
    }
}
