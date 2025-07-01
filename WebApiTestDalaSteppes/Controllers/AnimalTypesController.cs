using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace WebApiTestDalaSteppes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnimalTypesController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public AnimalTypesController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/AnimalTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalType>>> GetAnimalTypes()
        {
            return await _context.AnimalTypes.ToListAsync();
        }

        // GET: api/AnimalTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalType>> GetAnimalType(int id)
        {
            var animalType = await _context.AnimalTypes.FindAsync(id);

            if (animalType == null)
            {
                return NotFound();
            }

            return animalType;
        }

        // PUT: api/AnimalTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalType(int id, AnimalType animalType)
        {
            if (id != animalType.Id)
            {
                return BadRequest();
            }

            _context.Entry(animalType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!AnimalTypeExists(id))
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

        // POST: api/AnimalTypes
        [HttpPost]
        public async Task<ActionResult<AnimalType>> PostAnimalType(CreateAnimalType dto)
        {
            var animalType = new AnimalType
            {
                Name = dto.Name
            };
            _context.AnimalTypes.Add(animalType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimalType", new { id = animalType.Id }, animalType);
        }

        // DELETE: api/AnimalTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalType(int id)
        {
            var animalType = await _context.AnimalTypes.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }

            _context.AnimalTypes.Remove(animalType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalTypeExists(int id)
        {
            return _context.AnimalTypes.Any(e => e.Id == id);
        }
    }
}
