using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Genre;

namespace api.Repositories.Genre
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto?> GetGenreByIdAsync(int id);
        Task<GenreDto?> AddGenreAsync(UpsertGenreDto upsertGenreDto);
        Task<GenreDto?> UpdateGenreAsync(int id, UpsertGenreDto upsertGenreDto);
        Task<GenreDto?> DeleteGenreAsync(int id);
    }
}