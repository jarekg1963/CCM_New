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
    public class JgComplainStatusController : ControllerBase
    {
        private readonly CCMContext _context;

        public JgComplainStatusController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/JgComplainStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JgComplainStatus>>> GetJgComplainStatus()
        {
            return await _context.JgComplainStatus.ToListAsync();
        }

        [HttpGet("GetJgComplainStatusForComplain /{compId}")]
        public async Task<ActionResult<IEnumerable<JgComplainStatus>>> GetJgComplainStatusForComplain(int compId)
        {
            return await _context.JgComplainStatus.Where(c => c.IdCoplain == compId).ToListAsync();
        }

        // GET: api/JgComplainStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JgComplainStatus>> GetJgComplainStatus(int id)
        {
            var jgComplainStatus = await _context.JgComplainStatus.FindAsync(id);

            if (jgComplainStatus == null)
            {
                return NotFound();
            }

            return jgComplainStatus;
        }

        // PUT: api/JgComplainStatus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJgComplainStatus(int id, JgComplainStatus jgComplainStatus)
        {
            if (id != jgComplainStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(jgComplainStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JgComplainStatusExists(id))
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

        // POST: api/JgComplainStatus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<JgComplainStatus>> PostJgComplainStatus(JgComplainStatus jgComplainStatus)
        {
            _context.JgComplainStatus.Add(jgComplainStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJgComplainStatus", new { id = jgComplainStatus.Id }, jgComplainStatus);
        }

        // DELETE: api/JgComplainStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JgComplainStatus>> DeleteJgComplainStatus(int id)
        {
            var jgComplainStatus = await _context.JgComplainStatus.FindAsync(id);
            if (jgComplainStatus == null)
            {
                return NotFound();
            }

            _context.JgComplainStatus.Remove(jgComplainStatus);
            await _context.SaveChangesAsync();

            return jgComplainStatus;
        }

        private bool JgComplainStatusExists(int id)
        {
            return _context.JgComplainStatus.Any(e => e.Id == id);
        }
    }
}
