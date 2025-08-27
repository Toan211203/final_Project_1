using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Cart;

namespace api.Repositories.Cart
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartDTO>> GetAllCartsAsync();
        Task<CartDTO?> GetCartByIdAsync(int id);
        Task<CartDTO?> AddCartAsync(CreateCartDTO createCartDTO);
        Task<CartDTO?> UpdateCartAsync(int id, UpdateCartDTO updateCartDTO);
        Task<CartDTO?> DeleteCartAsync(int id);
        Task<IEnumerable<CartDTO>> GetCartByUserIdAsync(int userId);
        Task<IEnumerable<CartDTO>> DeleteCartByUserIdAsync(int userId);

    }
}