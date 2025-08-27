using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Repositories.Staff;
using api.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _staffRepository.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetStaffById(int userId)
        {
            var staff = await _staffRepository.GetStaffByIdAsync(userId);
            if (staff == null)
                return NotFound("Staff not found.");

            return Ok(staff);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffLesseDTO staffDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(staffDto.Email, emailPattern))
            {
                return BadRequest("The provided email is invalid.");
            }

            var existingUsername = await _staffRepository.GetUserByUsernameAsync(staffDto.Username);
            var existingEmail = await _staffRepository.GetUserByEmailAsync(staffDto.Email);

            if (existingUsername != null)
            {
                return Conflict(new { message = "Username already exists." });
            }

            if (existingEmail != null)
            {
                return Conflict(new { message = "Email already exists." });
            }

            var createdStaff = await _staffRepository.CreateStaffAsync(staffDto);
            if (createdStaff == null)
            {
                return Conflict(new { message = "Username already in use." });
            }

            return CreatedAtAction(nameof(GetStaffById), new { userId = createdStaff.UserId }, createdStaff);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateStaff(int userId, [FromBody] StaffLesseDTO staffDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingStaff = await _staffRepository.GetStaffByIdAsync(userId);
            if (existingStaff == null)
                return NotFound("Staff not found.");

            if (!string.Equals(existingStaff.Username, staffDto.Username, StringComparison.OrdinalIgnoreCase))
            {
                var userByUsername = await _staffRepository.GetUserByUsernameAsync(staffDto.Username);
                if (userByUsername != null)
                {
                    return Conflict(new { message = "Username already exists." });
                }
            }

            if (!string.Equals(existingStaff.Email, staffDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var userByEmail = await _staffRepository.GetUserByEmailAsync(staffDto.Email);
                if (userByEmail != null)
                {
                    return Conflict(new { message = "Email already exists." });
                }
            }

            var updatedStaff = await _staffRepository.UpdateStaffAsync(staffDto, userId);
            if (updatedStaff == null)
            {
                return Conflict(new { message = "Username already in use." });
            }

            return Ok(updatedStaff);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteStaff(int userId)
        {
            var deleted = await _staffRepository.DeleteStaffAsync(userId);
            if (!deleted)
                return NotFound("Staff not found or not a staff member.");

            return Ok("Staff deleted successfully.");
        }
    }
}