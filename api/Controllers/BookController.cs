using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Book;
using api.Repositories.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            var book = await _bookRepository.AddBookAsync(createBookDTO);
            return CreatedAtAction(nameof(GetBookById), new { id = book?.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            var book = await _bookRepository.UpdateBookAsync(id, updateBookDTO);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _bookRepository.DeleteBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string sort = "", [FromQuery] int limit = 0)
        {
            IEnumerable<BookDTO> books;

            switch (sort.ToLower())
            {
                case "newest":
                    books = await _bookRepository.GetNewestBooksAsync(limit);
                    break;
                case "popular":
                    books = await _bookRepository.GetPopularBooksAsync(limit);
                    break;
                default:
                    books = await _bookRepository.GetAllBooksAsync();
                    break;
            }
            return Ok(books);
        }

        // GET api/books/genre/{genreId}
        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByGenreId(int genreId)
        {
            var books = await _bookRepository.GetByGenreIdAsync(genreId);

            if (books == null || !books.Any())
            {
                return NotFound("No books found for the specified genre.");
            }

            return Ok(books);
        }
        
        // GET api/books/search/{title}
        [HttpGet("search/{title}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooksByTitle(string title)
        {
            var books = await _bookRepository.SearchBooksByTitleAsync(title);

            if (books == null || !books.Any())
            {
                return NotFound("No books found matching the specified title.");
            }

            return Ok(books);
        }
    }
}