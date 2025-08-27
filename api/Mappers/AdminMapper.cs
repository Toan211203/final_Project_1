using System;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class AdminMappers
    {
        public static AdminDTO ToAdminDTO(this User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

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

        public static User ToCreateAdminResponse(this CreateAdminDTO createAdminDTO)
        {
            if (createAdminDTO == null) throw new ArgumentNullException(nameof(createAdminDTO));

            return new User
            {
                Username = createAdminDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createAdminDTO.Password),
                FullName = createAdminDTO.FullName,
                Email = createAdminDTO.Email,
                Role = createAdminDTO.Role,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static User ToUpdateAdminResponse(this UpdateAdminDTO updateAdminDTO, User existingUser)
        {
            if (updateAdminDTO == null) throw new ArgumentNullException(nameof(updateAdminDTO));
            if (existingUser == null) throw new ArgumentNullException(nameof(existingUser));

            existingUser.Username = updateAdminDTO.Username;
            existingUser.FullName = updateAdminDTO.FullName;
            existingUser.Email = updateAdminDTO.Email;
            existingUser.Role = updateAdminDTO.Role;

            if (!string.IsNullOrWhiteSpace(updateAdminDTO.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateAdminDTO.Password);
            }

            return existingUser;
        }
    }
}