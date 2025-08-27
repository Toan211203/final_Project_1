using System;
using api.DTOs.Book;
using api.DTOs.Review;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDTO ToReviewDTO(this Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));

            return new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                BookId = review.BookId,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                Book = review.Book?.ToBookDTO() ?? new BookDTO(),
                User = review.User?.ToStaffLesseDTO() ?? new StaffLesseDTO()
            };
        }

        public static Review ToUpdateReviewResponse(this UpdateReviewDTO upsertReviewDTO)
        {
            if (upsertReviewDTO == null) throw new ArgumentNullException(nameof(upsertReviewDTO));

            return new Review
            {
                UserId = upsertReviewDTO.UserId,
                BookId = upsertReviewDTO.BookId,
                Rating = upsertReviewDTO.Rating,
                Comment = upsertReviewDTO.Comment
            };
        }

        public static Review ToCreateReviewResponse(this CreateReviewDTO upsertReviewDTO)
        {
            if (upsertReviewDTO == null) throw new ArgumentNullException(nameof(upsertReviewDTO));

            return new Review
            {
                UserId = upsertReviewDTO.UserId,
                BookId = upsertReviewDTO.BookId,
                Rating = upsertReviewDTO.Rating,
                Comment = upsertReviewDTO.Comment,
                ReviewDate = DateTime.UtcNow,
            };
        }
    }
}