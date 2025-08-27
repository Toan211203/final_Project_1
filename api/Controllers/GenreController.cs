using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Genre;
using api.Mappers;
using api.Repositories.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly BookRentalContext _context;
        private readonly IGenreRepository _genreRepo;

        public GenreController(BookRentalContext context, IGenreRepository genreRepo)
        {
            _context = context;
            _genreRepo = genreRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetGenres()
        {
            var genres = await _genreRepo.GetAllGenresAsync();
            return Ok(genres);
        }
        [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenreById(int id)
    {
        var genre = await _genreRepo.GetGenreByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

         [HttpPost]
        public async Task<ActionResult<GenreDto>> AddGenre(UpsertGenreDto upsertGenreDTO)
        {
            var genre = await _genreRepo.AddGenreAsync(upsertGenreDTO);
            return CreatedAtAction(nameof(GetGenreById), new { id = genre?.GenreId }, genre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDto>> UpdateGenre(int id, UpsertGenreDto upsertGenreDTO)
        {
            var genre = await _genreRepo.UpdateGenreAsync(id, upsertGenreDTO);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDto>> DeleteGenre(int id)
        {
            var genre = await _genreRepo.DeleteGenreAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
    }
}