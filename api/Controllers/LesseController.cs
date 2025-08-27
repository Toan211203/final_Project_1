using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Repositories.Lesse;
using api.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LesseController : ControllerBase
    {
        private readonly ILesseRepository _lesseRepository;

        public LesseController(ILesseRepository lesseRepository)
        {
            _lesseRepository = lesseRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLesse()
        {
            var lesse = await _lesseRepository.GetAllLesseAsync();
            return Ok(lesse);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetLesseById(int userId)
        {
            var lesse = await _lesseRepository.GetLesseByIdAsync(userId);
            if (lesse == null)
                return NotFound("Lesse not found.");

            return Ok(lesse);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLesse([FromBody] StaffLesseDTO lesseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(lesseDto.Email, emailPattern))
            {
                return BadRequest("The provided email is invalid.");
            }

            var existingUsername = await _lesseRepository.GetUserByUsernameAsync(lesseDto.Username);
            var existingEmail = await _lesseRepository.GetUserByEmailAsync(lesseDto.Email);

            if (existingUsername != null)
            {
                return Conflict(new { message = "Username already exists." });
            }

            if (existingEmail != null)
            {
                return Conflict(new { message = "Email already exists." });
            }

            var createdLesse = await _lesseRepository.CreateLesseAsync(lesseDto);
            if (createdLesse == null)
            {
                return Conflict(new { message = "Username already in use." });
            }

            return CreatedAtAction(nameof(GetLesseById), new { userId = createdLesse.UserId }, createdLesse);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateLesse(int userId, [FromBody] StaffLesseDTO lesseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLesse = await _lesseRepository.GetLesseByIdAsync(userId);
            if (existingLesse == null)
                return NotFound("Lesse not found.");

            if (!string.Equals(existingLesse.Username, lesseDto.Username, StringComparison.OrdinalIgnoreCase))
            {
                var userByUsername = await _lesseRepository.GetUserByUsernameAsync(lesseDto.Username);
                if (userByUsername != null)
                {
                    return Conflict(new { message = "Username already exists." });
                }
            }

            if (!string.Equals(existingLesse.Email, lesseDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var userByEmail = await _lesseRepository.GetUserByEmailAsync(lesseDto.Email);
                if (userByEmail != null)
                {
                    return Conflict(new { message = "Email already exists." });
                }
            }

            var updatedLesse = await _lesseRepository.UpdateLesseAsync(lesseDto, userId);
            if (updatedLesse == null)
            {
                return Conflict(new { message = "Username already in use." });
            }

            return Ok(updatedLesse);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteLesse(int userId)
        {
            var deleted = await _lesseRepository.DeleteLesseAsync(userId);
            if (!deleted)
                return NotFound("Lesse not found or not a lesse member.");

            return Ok("Lesse deleted successfully.");
        }
    }
}