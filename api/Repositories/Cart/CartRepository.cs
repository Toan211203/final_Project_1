using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Cart;
using api.DTOs.Users;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Cart
{
    public class CartRepository : ICartRepository
    {
        private readonly BookRentalContext _context;

        public CartRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartDTO>> GetAllCartsAsync()
        {
            var carts = await _context.Carts
                .Include(c => c.Book)
                .Include(c => c.User)
                .ToListAsync();

            return carts.Select(c => new CartDTO
            {
                CartId = c.CartId,
                UserId = c.UserId,
                BookId = c.BookId,
                Quantity = c.Quantity,
                AddedAt = c.AddedAt,
                Book = c.Book != null ? new BookDTO
                {
                    BookId = c.Book.BookId,
                    Title = c.Book.Title,
                    Author = c.Book.Author,
                    PublisherId = c.Book.PublisherId,
                    Isbn = c.Book.Isbn,
                    GenreId = c.Book.GenreId,
                    PublishedYear = c.Book.PublishedYear,
                    Quantity = c.Book.Quantity,
                    Price = c.Book.Price,
                    CreatedAt = c.Book.CreatedAt
                } : null,
                User = c.User != null ? new StaffLesseDTO
                {
                    UserId = c.User.UserId,
                    Username = c.User.Username
                } : null
            }).ToList();
        }

        public async Task<CartDTO?> GetCartByIdAsync(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CartId == id);

            if (cart == null) return null;

            return new CartDTO
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                BookId = cart.BookId,
                Quantity = cart.Quantity,
                AddedAt = cart.AddedAt,
                Book = cart.Book != null ? new BookDTO
                {
                    BookId = cart.Book.BookId,
                    Title = cart.Book.Title,
                    Author = cart.Book.Author,
                    PublisherId = cart.Book.PublisherId,
                    Isbn = cart.Book.Isbn,
                    GenreId = cart.Book.GenreId,
                    PublishedYear = cart.Book.PublishedYear,
                    Quantity = cart.Book.Quantity,
                    Price = cart.Book.Price,
                    CreatedAt = cart.Book.CreatedAt
                } : null,
                User = cart.User != null ? new StaffLesseDTO
                {
                    UserId = cart.User.UserId,
                    Username = cart.User.Username,
                } : null
            };
        }

        public async Task<CartDTO?> AddCartAsync(CreateCartDTO createCartDTO)
        {
            var cart = createCartDTO.ToCreateCartResponse();
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart.ToCartDTO();
        }

        public async Task<CartDTO?> UpdateCartAsync(int id, UpdateCartDTO updateCartDTO)
        {
            var existingCart = await _context.Carts.FindAsync(id);
            if (existingCart == null) return null;

            if (updateCartDTO.UserId > 0)
                existingCart.UserId = updateCartDTO.UserId;

            if (updateCartDTO.BookId > 0)
                existingCart.BookId = updateCartDTO.BookId;

            existingCart.Quantity = updateCartDTO.Quantity;

            _context.Carts.Update(existingCart);
            await _context.SaveChangesAsync();
            return existingCart.ToCartDTO();
        }

        public async Task<CartDTO?> DeleteCartAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return cart.ToCartDTO();
            }
            return null;
        }

        public async Task<IEnumerable<CartDTO>> GetCartByUserIdAsync(int userId)
        {
            var cartItems = await _context.Carts
                .Include(ci => ci.Book)
                .Include(ci => ci.User)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            return cartItems.Select(ci => new CartDTO
            {
                CartId = ci.CartId,
                UserId = ci.UserId,
                BookId = ci.BookId,
                Quantity = ci.Quantity,
                AddedAt = ci.AddedAt,
                Book = ci.Book != null ? new BookDTO
                {
                    BookId = ci.Book.BookId,
                    Title = ci.Book.Title,
                    Author = ci.Book.Author,
                    PublisherId = ci.Book.PublisherId,
                    Isbn = ci.Book.Isbn,
                    GenreId = ci.Book.GenreId,
                    PublishedYear = ci.Book.PublishedYear,
                    Quantity = ci.Book.Quantity,
                    Price = ci.Book.Price,
                    CreatedAt = ci.Book.CreatedAt
                } : null,
                User = ci.User != null ? new StaffLesseDTO
                {
                    UserId = ci.User.UserId,
                    Username = ci.User.Username
                } : null
            }).ToList();
        }
        
        public async Task<IEnumerable<CartDTO>> DeleteCartByUserIdAsync(int userId)
        {
            var carts = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (carts.Count == 0) return new List<CartDTO>(); 
            _context.Carts.RemoveRange(carts);
            await _context.SaveChangesAsync();
            return carts.Select(cart => cart.ToCartDTO()).ToList();
        }

    }
}