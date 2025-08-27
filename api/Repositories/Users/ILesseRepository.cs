using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Users;
using api.Models;

namespace api.Repositories.Users
{
    public interface ILesseRepository
    {
        Task<StaffLesseDTO> CreateLesseAsync(StaffLesseDTO lesseDto);
        Task<StaffLesseDTO?> GetLesseByIdAsync(int userId);
        Task<StaffLesseDTO?> UpdateLesseAsync(StaffLesseDTO lesseDto, int userId);
        Task<bool> DeleteLesseAsync(int userId);
        Task<IEnumerable<StaffLesseDTO>> GetAllLesseAsync();
        Task<bool> LesseExistAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
    }
}