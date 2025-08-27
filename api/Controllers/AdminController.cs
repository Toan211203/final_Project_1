using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Repositories.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAdminById(int userId)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(userId);
            if (admin == null)
                return NotFound("Admin not found.");

            return Ok(admin);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminDTO createAdminDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createAdminDTO.Email, emailPattern))
            {
                return BadRequest("The provided email is invalid.");
            }

            var existingUsername = await _adminRepository.GetUserByUsernameAsync(createAdminDTO.Username);
            var existingEmail = await _adminRepository.GetUserByEmailAsync(createAdminDTO.Email);

            if (existingUsername != null)
            {
                return Conflict(new { message = "Username already exists." });
            }

            if (existingEmail != null)
            {
                return Conflict(new { message = "Email already exists." });
            }

            var createdAdmin = await _adminRepository.CreateAdminAsync(createAdminDTO);
            if (createdAdmin == null)
            {
                return Conflict(new { message = "Username or email already in use." });
            }

            return CreatedAtAction(nameof(GetAdminById), new { userId = createdAdmin.UserId }, createdAdmin);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateAdmin(int userId, [FromBody] AdminDTO updateAdminDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAdmin = await _adminRepository.GetAdminByIdAsync(userId);
            if (existingAdmin == null)
                return NotFound("Admin not found.");

            if (!string.Equals(existingAdmin.Username, updateAdminDTO.Username, StringComparison.OrdinalIgnoreCase))
            {
                var userByUsername = await _adminRepository.GetUserByUsernameAsync(updateAdminDTO.Username);
                if (userByUsername != null)
                {
                    return Conflict(new { message = "Username already exists." });
                }
            }

            if (!string.Equals(existingAdmin.Email, updateAdminDTO.Email, StringComparison.OrdinalIgnoreCase))
            {
                var userByEmail = await _adminRepository.GetUserByEmailAsync(updateAdminDTO.Email);
                if (userByEmail != null)
                {
                    return Conflict(new { message = "Email already exists." });
                }
            }

            var updatedAdmin = await _adminRepository.UpdateAdminAsync(updateAdminDTO, userId);
            if (updatedAdmin == null)
            {
                return Conflict(new { message = "Username or email already in use." });
            }

            return Ok(updatedAdmin);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAdmin(int userId)
        {
            var deleted = await _adminRepository.DeleteAdminAsync(userId);
            if (!deleted)
                return NotFound("Admin not found or not an Admin.");

            return Ok("Admin deleted successfully.");
        }
    }
}