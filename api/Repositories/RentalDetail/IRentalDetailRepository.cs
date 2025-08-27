using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Rentals;

namespace api.Repositories.RentalDetail
{
    public interface IRentalDetailRepository
    {
        Task<IEnumerable<RentalDetailDTO>> GetAllRentalDetailsAsync();
        Task<RentalDetailDTO?> GetRentalDetailByIdAsync(int id);
        Task<RentalDetailDTO?> AddRentalDetailAsync(UpsertRentalDetailDTO upsertRentalDetailDTO);
        Task<RentalDetailDTO?> UpdateRentalDetailAsync(int id, UpsertRentalDetailDTO upsertRentalDetailDTO);
        Task<RentalDetailDTO?> DeleteRentalDetailAsync(int id);
    }
}