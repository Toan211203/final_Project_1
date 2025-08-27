using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Rentals;

namespace api.Repositories.Rental
{
    public interface IRentalRepository
    {
        Task<IEnumerable<RentalDTO>> GetAllRentalsAsync();
        Task<RentalDTO?> GetRentalByIdAsync(int id);
        Task<RentalDTO?> AddRentalAsync(CreateRentalDTO createRentalDTO);
        Task<RentalDTO?> UpdateRentalAsync(int id, UpdateRentalDTO updateRentalDTO);
        Task<RentalDTO?> DeleteRentalAsync(int id);
        Task<IEnumerable<RentalDTO>> GetRentalsByUserIdAsync(int userId);

    }
}