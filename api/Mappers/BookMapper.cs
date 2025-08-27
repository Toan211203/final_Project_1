using System;
using System.Collections.Generic;
using api.DTOs.Book;
using api.DTOs.Cart;
using api.DTOs.Publisher;
using api.DTOs.Rentals;
using api.DTOs.Review;
using api.Models;

namespace api.Mappers
{
    public static class BookMappers
    {
        public static BookDTO ToBookDTO(this Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

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
                Carts = book.Carts?.Select(c => c.ToCartDTO()).ToList() ?? new List<CartDTO>(),
                Genre = book.Genre?.ToGenreDTO(),
                Publisher = book.Publisher?.ToPublisherDTO() ?? new PublisherDTO(),
                RentalDetails = book.RentalDetails?.Select(rd => rd.ToRentalDetailDTO()).ToList() ?? new List<RentalDetailDTO>(),
                Reviews = book.Reviews?.Select(r => r.ToReviewDTO()).ToList() ?? new List<ReviewDTO>()
            };
        }

        public static Book ToCreateBookResponse(this CreateBookDTO createBookDTO)
        {
            if (createBookDTO == null) throw new ArgumentNullException(nameof(createBookDTO));

            return new Book
            {
                Title = createBookDTO.Title,
                Author = createBookDTO.Author,
                PublisherId = createBookDTO.PublisherId,
                Isbn = createBookDTO.Isbn,
                GenreId = createBookDTO.GenreId,
                PublishedYear = createBookDTO.PublishedYear,
                Quantity = createBookDTO.Quantity,
                Price = createBookDTO.Price,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Book ToUpdateBookResponse(this UpdateBookDTO updateBookDTO, Book existingBook)
        {
            if (updateBookDTO == null) throw new ArgumentNullException(nameof(updateBookDTO));
            if (existingBook == null) throw new ArgumentNullException(nameof(existingBook));

            existingBook.Title = updateBookDTO.Title;
            existingBook.Author = updateBookDTO.Author;
            existingBook.PublisherId = updateBookDTO.PublisherId;
            existingBook.Isbn = updateBookDTO.Isbn;
            existingBook.GenreId = updateBookDTO.GenreId;
            existingBook.PublishedYear = updateBookDTO.PublishedYear;
            existingBook.Quantity = updateBookDTO.Quantity;
            existingBook.Price = updateBookDTO.Price;

            return existingBook;
        }
    }
}