using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Book;

namespace api.DTOs.Genre
{
    public class GenreDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;
        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
    }

    public class UpsertGenreDto
    {
        public string GenreName { get; set; } = null!;
    }
}