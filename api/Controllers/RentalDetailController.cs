using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Rentals;
using api.Repositories.RentalDetail;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RentalDetailController : ControllerBase
    {
        private readonly IRentalDetailRepository _rentalDetailRepository;

        public RentalDetailController(IRentalDetailRepository rentalDetailRepository)
        {
            _rentalDetailRepository = rentalDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentalDetails()
        {
            var rentalDetails = await _rentalDetailRepository.GetAllRentalDetailsAsync();
            return Ok(rentalDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalDetailById([FromRoute] int id)
        {
            var rentalDetail = await _rentalDetailRepository.GetRentalDetailByIdAsync(id);
            if (rentalDetail == null)
            {
                return NotFound();
            }
            return Ok(rentalDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentalDetail([FromBody] UpsertRentalDetailDTO upsertRentalDetailDTO)
        {
            var rentalDetail = await _rentalDetailRepository.AddRentalDetailAsync(upsertRentalDetailDTO);
            return CreatedAtAction(nameof(GetRentalDetailById), new { id = rentalDetail?.RentalDetailId }, rentalDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRentalDetail([FromRoute] int id, [FromBody] UpsertRentalDetailDTO upsertRentalDetailDTO)
        {
            var rentalDetail = await _rentalDetailRepository.UpdateRentalDetailAsync(id, upsertRentalDetailDTO);
            if (rentalDetail == null)
            {
                return NotFound();
            }
            return Ok(rentalDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalDetail([FromRoute] int id)
        {
            var rentalDetail = await _rentalDetailRepository.DeleteRentalDetailAsync(id);
            if (rentalDetail == null)
            {
                return NotFound();
            }
            return Ok(rentalDetail);
        }
    }
}
