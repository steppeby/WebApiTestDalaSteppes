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
    public class BreedsController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public BreedsController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Breeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Breed>>> GetBreeds()
        {
            return await _context.Breeds.ToListAsync();
        }

        // GET: api/Breeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Breed>> GetBreed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if (breed == null)
            {
                return NotFound();
            }

            return breed;
        }

        // PUT: api/Breeds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreed(int id, Breed breed)
        {
            if (id != breed.Id)
            {
                return BadRequest();
            }

            _context.Entry(breed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!BreedExists(id))
                {
                    return NotFound();
                }
                if (!AnimalTypeExists(breed.AnimalTypeId))
                {
                    return BadRequest();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Breeds
        [HttpPost]
        public async Task<ActionResult<Breed>> PostBreed(CreateBreed dto)
        {
            var breed = new Breed
            {
                Name = dto.Name,
                AnimalTypeId = dto.AnimalTypeId
            };
            
            _context.Breeds.Add(breed);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!AnimalTypeExists(breed.AnimalTypeId))
                {
                    return BadRequest("Animal type not exist.");
                }
                throw;
            }
            return CreatedAtAction("GetBreed", new { id = breed.Id }, breed);
        }

        // DELETE: api/Breeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BreedExists(int id)
        {
            return _context.Breeds.Any(e => e.Id == id);
        }
        private bool AnimalTypeExists(int id)
        {
            return _context.AnimalTypes.Any(e => e.Id == id);
        }
    }
}
