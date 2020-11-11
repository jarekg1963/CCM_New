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
    public class TblSoesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblSoesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblSoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSo>>> GetTblSo()
        {
            return await _context.TblSo.ToListAsync();
        }

        // GET: api/TblSoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSo>> GetTblSo(int id)
        {
            var tblSo = await _context.TblSo.FindAsync(id);

            if (tblSo == null)
            {
                return NotFound();
            }

            return tblSo;
        }

        // PUT: api/TblSoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSo(int id, TblSo tblSo)
        {
            if (id != tblSo.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblSo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSoExists(id))
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

        // POST: api/TblSoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblSo>> PostTblSo(TblSo tblSo)
        {
            _context.TblSo.Add(tblSo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblSo", new { id = tblSo.Id }, tblSo);
        }

        // DELETE: api/TblSoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblSo>> DeleteTblSo(int id)
        {
            var tblSo = await _context.TblSo.FindAsync(id);
            if (tblSo == null)
            {
                return NotFound();
            }

            _context.TblSo.Remove(tblSo);
            await _context.SaveChangesAsync();

            return tblSo;
        }

        private bool TblSoExists(int id)
        {
            return _context.TblSo.Any(e => e.Id == id);
        }
    }
}
