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
    public class TblTasksController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblTasksController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTasks>>> GetTblTasks()
        {
            return await _context.TblTasks.ToListAsync();
        }

        // GET: api/TblTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTasks>> GetTblTasks(int id)
        {
            var tblTasks = await _context.TblTasks.FindAsync(id);

            if (tblTasks == null)
            {
                return NotFound();
            }

            return tblTasks;
        }

        // PUT: api/TblTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTasks(int id, TblTasks tblTasks)
        {
            if (id != tblTasks.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(tblTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblTasksExists(id))
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

        // POST: api/TblTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblTasks>> PostTblTasks(TblTasks tblTasks)
        {
            _context.TblTasks.Add(tblTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblTasks", new { id = tblTasks.TaskId }, tblTasks);
        }

        // DELETE: api/TblTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblTasks>> DeleteTblTasks(int id)
        {
            var tblTasks = await _context.TblTasks.FindAsync(id);
            if (tblTasks == null)
            {
                return NotFound();
            }

            _context.TblTasks.Remove(tblTasks);
            await _context.SaveChangesAsync();

            return tblTasks;
        }

        private bool TblTasksExists(int id)
        {
            return _context.TblTasks.Any(e => e.TaskId == id);
        }
    }
}
