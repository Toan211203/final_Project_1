using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Genre;
using api.DTOs.Publisher;
using api.DTOs.Review;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Book
{
    public class BookRepository : IBookRepository
    {
        private readonly BookRentalContext _context;

        public BookRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }

       public async Task<BookDTO?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return null;

            return new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublisherId = book.PublisherId,
                Isbn = book.Isbn,
                GenreId = book.GenreId,
                PublishedYear = book.PublishedYear,
                Quantity = book.Quantity,
                Price = book.Price,
                CreatedAt = book.CreatedAt,
                GenreName = book.Genre != null ? book.Genre.GenreName : "N/A",
                PublisherName = book.Publisher != null ? book.Publisher.PublisherName : "N/A",
                Reviews = book.Reviews
                    .Where(r => r.BookId == book.BookId)
                    .Select(r => new ReviewDTO
                    {
                        ReviewId = r.ReviewId,
                        UserId = r.UserId,
                        BookId = r.BookId,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        ReviewDate = r.ReviewDate
                    }).ToList()
            };
        }

        public async Task<BookDTO?> AddBookAsync(CreateBookDTO createBookDTO)
        {
            var book = createBookDTO.ToCreateBookResponse();
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book.ToBookDTO();
        }

        public async Task<BookDTO?> UpdateBookAsync(int id, UpdateBookDTO updateBookDTO)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null) return null;

            if (!string.IsNullOrEmpty(updateBookDTO.Title))
                existingBook.Title = updateBookDTO.Title;

            if (!string.IsNullOrEmpty(updateBookDTO.Author))
                existingBook.Author = updateBookDTO.Author;

            if (updateBookDTO.PublisherId > 0)
                existingBook.PublisherId = updateBookDTO.PublisherId;

            if (!string.IsNullOrEmpty(updateBookDTO.Isbn))
                existingBook.Isbn = updateBookDTO.Isbn;

            if (updateBookDTO.GenreId.HasValue)
                existingBook.GenreId = updateBookDTO.GenreId;

            if (updateBookDTO.PublishedYear.HasValue)
                existingBook.PublishedYear = updateBookDTO.PublishedYear;

            existingBook.Quantity = updateBookDTO.Quantity;
            existingBook.Price = updateBookDTO.Price;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
            return existingBook.ToBookDTO();
        }

        public async Task<IEnumerable<BookDTO>> GetNewestBooksAsync(int limit)
        {
            var query = _context.Books
                .OrderByDescending(b => b.CreatedAt)
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            var books = await query.ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<BookDTO>> GetPopularBooksAsync(int limit)
        {
            var query = _context.Books
                .OrderByDescending(b => b.Quantity)
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            var books = await query.ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }

        public async Task<BookDTO?> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return book.ToBookDTO();
            }
            return null;
        }

        public async Task<IEnumerable<BookDTO>> GetByGenreIdAsync(int genreId)
        {
            var books = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Where(b => b.GenreId == genreId)
                .ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<BookDTO>> GetByPublisherIdAsync(int publisherId)
        {
            var books = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Where(b => b.PublisherId == publisherId)
                .ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }
        
        public async Task<IEnumerable<BookDTO>> SearchBooksByTitleAsync(string title)
        {
            var books = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Where(b => b.Title.Contains(title))
                .ToListAsync();

            return books.Select(b => new BookDTO
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
                CreatedAt = b.CreatedAt,
                GenreName = b.Genre != null ? b.Genre.GenreName : "N/A",
                PublisherName = b.Publisher != null ? b.Publisher.PublisherName : "N/A",
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }
    }
}