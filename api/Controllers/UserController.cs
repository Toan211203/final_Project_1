using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Login;
using api.Dtos.Register;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BookRentalContext _context;
        private readonly IConfiguration _configuration;

        public UserController(BookRentalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Address = dto.Address,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDTO.Username);
            if (user == null)
                return Unauthorized("User not found or invalid credentials.");

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
                return Unauthorized("User or password is incorrect.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Username", user.Username),
                new Claim("role", user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenValue, userId = user.UserId, username = user.Username, role = user.Role });
        }
    }
}