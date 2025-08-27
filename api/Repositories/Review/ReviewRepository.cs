using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Review;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Review
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookRentalContext _context;

        public ReviewRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();

            return reviews.Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                Username = r.User.FullName,
                Rating = r.Rating,
                Comment = r.Comment,
                ReviewDate = r.ReviewDate,
                Book = new BookDTO
                {
                    BookId = r.Book.BookId,
                    Title = r.Book.Title,
                    Author = r.Book.Author,
                    PublisherId = r.Book.PublisherId,
                    Isbn = r.Book.Isbn,
                    GenreId = r.Book.GenreId,
                    PublishedYear = r.Book.PublishedYear,
                    Quantity = r.Book.Quantity,
                    Price = r.Book.Price,
                    CreatedAt = r.Book.CreatedAt
                }
            }).ToList();
        }

        public async Task<ReviewDTO?> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null) return null;

            return new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                BookId = review.BookId,
                BookTitle = review.Book.Title,
                Username = review.User.FullName,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                Book = new BookDTO
                {
                    BookId = review.Book.BookId,
                    Title = review.Book.Title,
                    Author = review.Book.Author,
                    PublisherId = review.Book.PublisherId,
                    Isbn = review.Book.Isbn,
                    GenreId = review.Book.GenreId,
                    PublishedYear = review.Book.PublishedYear,
                    Quantity = review.Book.Quantity,
                    Price = review.Book.Price,
                    CreatedAt = review.Book.CreatedAt
                }
            };
        }

        public async Task<ReviewDTO?> AddReviewAsync(CreateReviewDTO upsertReviewDTO)
        {
            var review = upsertReviewDTO.ToCreateReviewResponse();
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review.ToReviewDTO();
        }

        public async Task<ReviewDTO?> UpdateReviewAsync(int id, UpdateReviewDTO upsertReviewDTO)
        {
            var existingReview = await _context.Reviews.FindAsync(id);
            if (existingReview == null) return null;

            existingReview.UserId = upsertReviewDTO.UserId;
            existingReview.BookId = upsertReviewDTO.BookId;
            existingReview.Rating = upsertReviewDTO.Rating;
            existingReview.Comment = upsertReviewDTO.Comment;

            _context.Reviews.Update(existingReview);
            await _context.SaveChangesAsync();
            return existingReview.ToReviewDTO();
        }

        public async Task<ReviewDTO?> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return review.ToReviewDTO();
            }
            return null;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByUserAsync(int userId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return reviews.Select(review => new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                BookId = review.BookId,
                BookTitle = review.Book.Title,
                Username = review.User.FullName,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                Book = new BookDTO
                {
                    BookId = review.Book.BookId,
                    Title = review.Book.Title,
                    Author = review.Book.Author,
                    PublisherId = review.Book.PublisherId,
                    Isbn = review.Book.Isbn,
                    GenreId = review.Book.GenreId,
                    PublishedYear = review.Book.PublishedYear,
                    Quantity = review.Book.Quantity,
                    Price = review.Book.Price,
                    CreatedAt = review.Book.CreatedAt
                }
            });
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByBookAsync(int bookId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .ToListAsync();

            return reviews.Select(review => new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                FullName = review.User.FullName,
                BookId = review.BookId,
                BookTitle = review.Book.Title,
                Username = review.User.Username,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate
            }).ToList();
        }
    }
}