using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Data;

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
        [Authorize]
        public async Task<ActionResult<Weighting>> GetWeighting(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            IQueryable<Weighting> query = _context.Weightings;
            if (!roles.Contains("Admin"))
            {
                query = query.Where(w =>  w.AssignedToUserId == user.Id && w.Id == id);
            }
            else
            {
                query = query.Where(w => w.Id == id);
            }
            var weighting = await query.FirstOrDefaultAsync();

            if (weighting == null)
            {
                return NotFound();
            }

            return weighting;
        }

        // PUT: api/Weightings/5
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
            catch (DbUpdateException)
            {
                if (!WeightingExists(id))
                {
                    return NotFound();
                }
                if (!AnimalExists(weighting.AnimalId))
                {
                    return NotFound("Animal does not exist.");
                }
                if (!AssignedUserExists(weighting.AssignedToUserId))
                {
                    return NotFound("Assigned user does not exist.");
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Weightings
        [HttpPost]
        public async Task<ActionResult<Weighting>> PostWeighting(CreateWeighting dto)
        {
            var weighting = new Weighting
            {
                AnimalId = dto.AnimalId,
                Weight = dto.Weight,
                WeightDate = dto.WeightDate,
                AssignedToUserId = dto.AssignedToUserId
            };
            _context.Weightings.Add(weighting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!AnimalExists(weighting.AnimalId))
                {
                    return NotFound("Animal does not exist.");
                }
                if (!AssignedUserExists(weighting.AssignedToUserId))
                {
                    return NotFound("Assigned user does not exist.");
                }
                if (!NotUniqueWeighting(weighting.AnimalId, weighting.WeightDate))
                {
                    return BadRequest("You cannot weigh an animal twice on the same date.");
                }
                throw;
            }

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
        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
        private bool AssignedUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool NotUniqueWeighting(int animalId, DateTime weightingDate)
        {
            return _context.Weightings.Any(e => e.AnimalId == animalId && e.WeightDate == weightingDate);
        }
    }
}
