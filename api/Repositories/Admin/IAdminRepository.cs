using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Models;

namespace api.Repositories.Admin
{
    public interface IAdminRepository
    {
        Task<AdminDTO> CreateAdminAsync(AdminDTO adminDto);
        Task<AdminDTO?> GetAdminByIdAsync(int userId);
        Task<AdminDTO?> UpdateAdminAsync(AdminDTO adminDto, int userId);
        Task<bool> DeleteAdminAsync(int userId);
        Task<IEnumerable<AdminDTO>> GetAllAdminsAsync();
        Task<bool> AdminExistAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);

    }
}