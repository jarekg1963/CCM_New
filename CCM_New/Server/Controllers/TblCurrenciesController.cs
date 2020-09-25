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
    public class TblCurrenciesController : ControllerBase
    {
        private readonly CCMContext _context;

        public TblCurrenciesController(CCMContext context)
        {
            _context = context;
        }

        // GET: api/TblCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCurrencies>>> GetTblCurrencies()
        {
            return await _context.TblCurrencies.ToListAsync();
        }

        // GET: api/TblCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCurrencies>> GetTblCurrencies(int id)
        {
            var tblCurrencies = await _context.TblCurrencies.FindAsync(id);

            if (tblCurrencies == null)
            {
                return NotFound();
            }

            return tblCurrencies;
        }

        // PUT: api/TblCurrencies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCurrencies(int id, TblCurrencies tblCurrencies)
        {
            if (id != tblCurrencies.CurrencyId)
            {
                return BadRequest();
            }

            _context.Entry(tblCurrencies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCurrenciesExists(id))
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

        // POST: api/TblCurrencies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblCurrencies>> PostTblCurrencies(TblCurrencies tblCurrencies)
        {
            _context.TblCurrencies.Add(tblCurrencies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCurrencies", new { id = tblCurrencies.CurrencyId }, tblCurrencies);
        }

        // DELETE: api/TblCurrencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblCurrencies>> DeleteTblCurrencies(int id)
        {
            var tblCurrencies = await _context.TblCurrencies.FindAsync(id);
            if (tblCurrencies == null)
            {
                return NotFound();
            }

            _context.TblCurrencies.Remove(tblCurrencies);
            await _context.SaveChangesAsync();

            return tblCurrencies;
        }

        private bool TblCurrenciesExists(int id)
        {
            return _context.TblCurrencies.Any(e => e.CurrencyId == id);
        }
    }
}
