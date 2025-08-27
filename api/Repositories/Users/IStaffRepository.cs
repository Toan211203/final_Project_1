using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Models;

namespace api.Repositories.Users
{
    public interface IStaffRepository
    {
        Task<StaffLesseDTO> CreateStaffAsync(StaffLesseDTO staffDto);
        Task<StaffLesseDTO?> GetStaffByIdAsync(int userId);
        Task<StaffLesseDTO?> UpdateStaffAsync(StaffLesseDTO staffDto, int userId);
        Task<bool> DeleteStaffAsync(int userId);
        Task<IEnumerable<StaffLesseDTO>> GetAllStaffAsync();
        Task<bool> StaffExistAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
    }
}