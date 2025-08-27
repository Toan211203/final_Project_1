using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Users;
using api.Models;
using api.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Lesse
{
    public class LesseRepository : ILesseRepository
    {
        private readonly BookRentalContext _context;

        public LesseRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<StaffLesseDTO> CreateLesseAsync(StaffLesseDTO lesseDto)
        {
            var existingUserByUsername = await GetUserByUsernameAsync(lesseDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }

            var user = new User
            {
                Username = lesseDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(lesseDto.Password),
                FullName = lesseDto.FullName,
                Address = lesseDto.Address,
                Email = lesseDto.Email,
                PhoneNumber = lesseDto.PhoneNumber,
                Role = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new StaffLesseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<StaffLesseDTO?> GetLesseByIdAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId && u.Role == 0);

            if (user == null) return null;

            return new StaffLesseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<StaffLesseDTO?> UpdateLesseAsync(StaffLesseDTO lesseDto, int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null || existingUser.Role != 0)
            {
                return null;
            }

            existingUser.FullName = lesseDto.FullName;
            existingUser.Address = lesseDto.Address;
            existingUser.Email = lesseDto.Email;
            existingUser.PhoneNumber = lesseDto.PhoneNumber;

            if (!string.IsNullOrEmpty(lesseDto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(lesseDto.Password);
            }

            await _context.SaveChangesAsync();

            return new StaffLesseDTO
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username,
                Password = existingUser.Password,
                FullName = existingUser.FullName,
                Address = existingUser.Address,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Role = existingUser.Role,
                CreatedAt = existingUser.CreatedAt
            };
        }

        public async Task<bool> DeleteLesseAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != 0)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StaffLesseDTO>> GetAllLesseAsync()
        {
            return await _context.Users
                .Where(u => u.Role == 0)
                .Select(u => new StaffLesseDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password,
                    FullName = u.FullName,
                    Address = u.Address,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<bool> LesseExistAsync(int userId)
        {
            return await _context.Users
                .AnyAsync(u => u.UserId == userId && u.Role == 0);
        }
        
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}