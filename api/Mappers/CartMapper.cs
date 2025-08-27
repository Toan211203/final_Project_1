using System;
using api.DTOs.Book;
using api.DTOs.Cart;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class CartMappers
    {
        public static CartDTO ToCartDTO(this Cart cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            return new CartDTO
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                BookId = cart.BookId,
                Quantity = cart.Quantity,
                AddedAt = cart.AddedAt,
                Book = cart.Book?.ToBookDTO() ?? new BookDTO(),
                User = cart.User?.ToStaffLesseDTO() ?? new StaffLesseDTO()
            };
        }

        public static Cart ToCreateCartResponse(this CreateCartDTO createCartDTO)
        {
            if (createCartDTO == null) throw new ArgumentNullException(nameof(createCartDTO));

            return new Cart
            {
                UserId = createCartDTO.UserId,
                BookId = createCartDTO.BookId,
                Quantity = createCartDTO.Quantity,
                AddedAt = DateTime.UtcNow
            };
        }

        public static Cart ToUpdateCartResponse(this UpdateCartDTO updateCartDTO, Cart existingCart)
        {
            if (updateCartDTO == null) throw new ArgumentNullException(nameof(updateCartDTO));
            if (existingCart == null) throw new ArgumentNullException(nameof(existingCart));

            existingCart.UserId = updateCartDTO.UserId;
            existingCart.BookId = updateCartDTO.BookId;
            existingCart.Quantity = updateCartDTO.Quantity;

            return existingCart;
        }
    }
}