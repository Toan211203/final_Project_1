using System;
using api.DTOs.Book;
using api.DTOs.Genre;
using api.Models;

namespace api.Mappers
{
    public static class GenreMappers
    {
        public static GenreDto ToGenreDTO(this Genre genre)
        {
            if (genre == null) throw new ArgumentNullException(nameof(genre));

            return new GenreDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Books = genre.Books?.Select(b => b.ToBookDTO()).ToList() ?? new List<BookDTO>()
            };
        }

        public static Genre ToCreateGenreResponse(this UpsertGenreDto upsertGenreDTO)
        {
            if (upsertGenreDTO == null) throw new ArgumentNullException(nameof(upsertGenreDTO));

            return new Genre
            {
                GenreName = upsertGenreDTO.GenreName
            };
        }

        public static Genre ToUpdateGenreResponse(this UpsertGenreDto upsertGenreDTO, Genre existingGenre)
        {
            if (upsertGenreDTO == null) throw new ArgumentNullException(nameof(upsertGenreDTO));
            if (existingGenre == null) throw new ArgumentNullException(nameof(existingGenre));

            existingGenre.GenreName = upsertGenreDTO.GenreName;
            return existingGenre;
        }
    }
}