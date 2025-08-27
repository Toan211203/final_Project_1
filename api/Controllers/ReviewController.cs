using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Review;
using api.Repositories.Review;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById([FromRoute] int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO upsertReviewDTO)
        {
            var review = await _reviewRepository.AddReviewAsync(upsertReviewDTO);
            return CreatedAtAction(nameof(GetReviewById), new { id = review?.ReviewId }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] int id, [FromBody] UpdateReviewDTO upsertReviewDTO)
        {
            var review = await _reviewRepository.UpdateReviewAsync(id, upsertReviewDTO);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var review = await _reviewRepository.DeleteReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUser(int userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserAsync(userId);
            return Ok(reviews);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsByBook(int bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookAsync(bookId);
            return Ok(reviews);
        }
    }
}

