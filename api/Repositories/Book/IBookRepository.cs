using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Book;

namespace api.Repositories.Book
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<BookDTO?> GetBookByIdAsync(int id);
        Task<BookDTO?> AddBookAsync(CreateBookDTO createBookDTO);
        Task<BookDTO?> UpdateBookAsync(int id, UpdateBookDTO updateBookDTO);
        Task<BookDTO?> DeleteBookAsync(int id);

        Task<IEnumerable<BookDTO>> GetNewestBooksAsync(int limit);
        Task<IEnumerable<BookDTO>> GetPopularBooksAsync(int limit);
        Task<IEnumerable<BookDTO>> GetByGenreIdAsync(int genreId);
        Task<IEnumerable<BookDTO>> GetByPublisherIdAsync(int publisherId);
        Task<IEnumerable<BookDTO>> SearchBooksByTitleAsync(string title);
    }
}