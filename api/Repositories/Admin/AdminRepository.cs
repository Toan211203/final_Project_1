using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Users;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BookRentalContext _context;

        public AdminRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<AdminDTO> CreateAdminAsync(AdminDTO adminDto)
        {
            var existingUserByUsername = await GetUserByUsernameAsync(adminDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }

            var existingUserByEmail = await GetUserByEmailAsync(adminDto.Email);
            if (existingUserByEmail != null)
            {
                return null;
            }

            var user = new User
            {
                Username = adminDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password),
                FullName = adminDto.FullName,
                Email = adminDto.Email,
                Role = 1,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AdminDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password= user.Password,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<AdminDTO?> GetAdminByIdAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId && u.Role == 1);

            if (user == null) return null;

            return new AdminDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<AdminDTO?> UpdateAdminAsync(AdminDTO adminDto, int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null || existingUser.Role != 1)
            {
                return null;
            }

            if (!string.Equals(existingUser.Username, adminDto.Username, StringComparison.OrdinalIgnoreCase))
            {
                var userByUsername = await GetUserByUsernameAsync(adminDto.Username);
                if (userByUsername != null && userByUsername.UserId != userId)
                {
                    return null;
                }
            }

            if (!string.Equals(existingUser.Email, adminDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var userByEmail = await GetUserByEmailAsync(adminDto.Email);
                if (userByEmail != null && userByEmail.UserId != userId)
                {
                    return null;
                }
            }

            existingUser.Username = adminDto.Username;
            existingUser.FullName = adminDto.FullName;
            existingUser.Email = adminDto.Email;
            existingUser.Role = 1;

            if (!string.IsNullOrEmpty(adminDto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password);
            }

            await _context.SaveChangesAsync();

            return new AdminDTO
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username,
                Password = existingUser.Password,
                FullName = existingUser.FullName,
                Email = existingUser.Email,
                Role = existingUser.Role,
                CreatedAt = existingUser.CreatedAt
            };
        }

        public async Task<bool> DeleteAdminAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != 1)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AdminDTO>> GetAllAdminsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == 1) 
                .Select(u => new AdminDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<bool> AdminExistAsync(int userId)
        {
            return await _context.Users
                .AnyAsync(u => u.UserId == userId && u.Role == 1);
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