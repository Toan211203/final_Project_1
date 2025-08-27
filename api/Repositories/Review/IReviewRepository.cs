using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Review;

namespace api.Repositories.Review
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO?> GetReviewByIdAsync(int id);
        Task<ReviewDTO?> AddReviewAsync(CreateReviewDTO upsertReviewDTO);
        Task<ReviewDTO?> UpdateReviewAsync(int id, UpdateReviewDTO upsertReviewDTO);
        Task<ReviewDTO?> DeleteReviewAsync(int id);
        Task<IEnumerable<ReviewDTO>> GetReviewsByUserAsync(int userId);
        Task<IEnumerable<ReviewDTO>> GetReviewsByBookAsync(int bookId);
    }
}