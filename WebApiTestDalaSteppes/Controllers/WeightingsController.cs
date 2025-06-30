using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTestDalaSteppes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeightingsController : ControllerBase
    {
        private readonly WebApiDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WeightingsController(UserManager<IdentityUser> userManager, WebApiDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Weightings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weighting>>> GetWeightings()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            IQueryable<Weighting> query = _context.Weightings;

            if (!roles.Contains("Admin"))
            {
                query = query.Where(w => w.AssignedToUserId == user.Id);
            }

            var weightings = await query.ToListAsync();

            return Ok(weightings);
        }

        // GET: api/Weightings/5
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
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
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
            {
                if (user.Id != weighting.AssignedToUserId)
                {
                    return Forbid();
                }
            }
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
        [Authorize(Roles = "Admin")]
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
