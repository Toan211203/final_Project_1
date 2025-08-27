using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Rentals;
using api.Repositories.Rental;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById([FromRoute] int id)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalDTO createRentalDTO)
        {
            // var userIdClaim = User.FindFirst("UserId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User not authenticated.");
            // }
            // var userId = int.Parse(userIdClaim.Value);
            // createRentalDTO.UserId = userId;
            var rental = await _rentalRepository.AddRentalAsync(createRentalDTO);
            return CreatedAtAction(nameof(GetRentalById), new { id = rental?.RentalId }, rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental([FromRoute] int id, [FromBody] UpdateRentalDTO updateRentalDTO)
        {
            var rental = await _rentalRepository.UpdateRentalAsync(id, updateRentalDTO);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental([FromRoute] int id)
        {
            var rental = await _rentalRepository.DeleteRentalAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRentalsByUserId([FromRoute] int userId)
        {
            var rentals = await _rentalRepository.GetRentalsByUserIdAsync(userId);
            if (rentals == null || !rentals.Any())
            {
                return NotFound($"No rental with user id {userId} found.");
            }
            return Ok(rentals);
        }

    }
}
