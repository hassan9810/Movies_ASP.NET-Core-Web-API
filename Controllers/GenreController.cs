using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDTO dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };
            await _context.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GenreDTO dTO)
        {
            var genre = await _context.Genres.
                                SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($"No Genere was found with suh ID: {id}");
            }
            else
            {
                genre.Name=dTO.Name;
            }
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _context.Genres.
                               SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($"No Genere was found with suh ID: {id}");
            }
            else
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            return Ok(genre);
        }
    }
}
