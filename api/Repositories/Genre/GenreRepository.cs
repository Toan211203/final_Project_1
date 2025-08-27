using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Genre;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Genre
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookRentalContext _context;
        private readonly ILogger<GenreRepository> _logger;

        public GenreRepository(BookRentalContext context, ILogger<GenreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _context.Genres
                .Include(g => g.Books)
                .ToListAsync();

            return genres.Select(genre => new GenreDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Books = genre.Books.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author
                }).ToList()
            }).ToList();
        }

        public async Task<GenreDto?> GetGenreByIdAsync(int genreId)
        {
            var genre = await _context.Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.GenreId == genreId);

            if (genre == null) return null;

            return new GenreDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Books = genre.Books.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    PublisherId = b.PublisherId,
                    Isbn = b.Isbn,
                    GenreId = b.GenreId,
                    PublishedYear = b.PublishedYear,
                    Quantity = b.Quantity,
                    Price = b.Price,
                    CreatedAt = b.CreatedAt
                }).ToList()
            };
        }

        public async Task<GenreDto?> AddGenreAsync(UpsertGenreDto upsertGenreDTO)
        {
            var genre = upsertGenreDTO.ToCreateGenreResponse();
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre.ToGenreDTO();
        }

        public async Task<GenreDto?> UpdateGenreAsync(int id, UpsertGenreDto upsertGenreDTO)
        {
            var existingGenre = await _context.Genres.FindAsync(id);
            if (existingGenre == null) return null;

            if (!string.IsNullOrEmpty(upsertGenreDTO.GenreName))
                existingGenre.GenreName = upsertGenreDTO.GenreName;

            _context.Genres.Update(existingGenre);
            await _context.SaveChangesAsync();
            return existingGenre.ToGenreDTO();
        }

        public async Task<GenreDto?> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                return genre.ToGenreDTO();
            }
            return null;
        }
    }
}