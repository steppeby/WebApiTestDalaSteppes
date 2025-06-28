using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApiTestDalaSteppes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightingsController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public WeightingsController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Weightings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weighting>>> GetWeightings()
        {
            return await _context.Weightings.ToListAsync();
        }

        // GET: api/Weightings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weighting>> GetWeighting(int id)
        {
            var weighting = await _context.Weightings.FindAsync(id);

            if (weighting == null)
            {
                return NotFound();
            }

            return weighting;
        }

        // PUT: api/Weightings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeighting(int id, Weighting weighting)
        {
            if (id != weighting.Id)
            {
                return BadRequest();
            }

            _context.Entry(weighting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightingExists(id))
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

        // POST: api/Weightings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weighting>> PostWeighting(Weighting weighting)
        {
            _context.Weightings.Add(weighting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeighting", new { id = weighting.Id }, weighting);
        }

        // DELETE: api/Weightings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeighting(int id)
        {
            var weighting = await _context.Weightings.FindAsync(id);
            if (weighting == null)
            {
                return NotFound();
            }

            _context.Weightings.Remove(weighting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeightingExists(int id)
        {
            return _context.Weightings.Any(e => e.Id == id);
        }
    }
}
