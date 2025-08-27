using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Users;
using api.Models;
using api.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Staff
{
    public class StaffRepository : IStaffRepository
    {
        private readonly BookRentalContext _context;

        public StaffRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<StaffLesseDTO> CreateStaffAsync(StaffLesseDTO staffDto)
        {
            var existingUserByUsername = await GetUserByUsernameAsync(staffDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }

            var user = new User
            {
                Username = staffDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(staffDto.Password),
                FullName = staffDto.FullName,
                Address = staffDto.Address,
                Email = staffDto.Email,
                PhoneNumber = staffDto.PhoneNumber,
                Role = 2,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

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

        public async Task<StaffLesseDTO?> GetStaffByIdAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId && u.Role == 2);

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

        public async Task<StaffLesseDTO?> UpdateStaffAsync(StaffLesseDTO staffDto, int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null || existingUser.Role != 2)
            {
                return null;
            }
            existingUser.FullName = staffDto.FullName;
            existingUser.Address = staffDto.Address;
            existingUser.Email = staffDto.Email;
            existingUser.PhoneNumber = staffDto.PhoneNumber;

            if (!string.IsNullOrEmpty(staffDto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(staffDto.Password);
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

        public async Task<bool> DeleteStaffAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != 2)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StaffLesseDTO>> GetAllStaffAsync()
        {
            return await _context.Users
                .Where(u => u.Role == 2)
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

        public async Task<bool> StaffExistAsync(int userId)
        {
            return await _context.Users
                .AnyAsync(u => u.UserId == userId && u.Role == 2);
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