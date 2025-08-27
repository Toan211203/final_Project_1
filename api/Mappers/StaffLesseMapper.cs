using System;
using System.Collections.Generic;
using api.DTOs.Cart;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.DTOs.Review;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class StaffLesseeMappers
    {
        public static StaffLesseDTO ToStaffLesseDTO(this User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new StaffLesseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                Carts = user.Carts?.Select(c => c.ToCartDTO()).ToList() ?? new List<CartDTO>(),
                Invoices = user.Invoices?.Select(i => i.ToInvoiceDTO()).ToList() ?? new List<InvoiceDTO>(),
                Rentals = user.Rentals?.Select(r => r.ToRentalDTO()).ToList() ?? new List<RentalDTO>(),
                Reviews = user.Reviews?.Select(r => r.ToReviewDTO()).ToList() ?? new List<ReviewDTO>()
            };
        }

        public static User ToCreateStaffLesseeResponse(this CreateStaffLesseeDTO createStaffLesseeDTO)
        {
            if (createStaffLesseeDTO == null) throw new ArgumentNullException(nameof(createStaffLesseeDTO));

            return new User
            {
                Username = createStaffLesseeDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createStaffLesseeDTO.Password),
                FullName = createStaffLesseeDTO.FullName,
                Address = createStaffLesseeDTO.Address,
                Email = createStaffLesseeDTO.Email,
                PhoneNumber = createStaffLesseeDTO.PhoneNumber,
                Role = createStaffLesseeDTO.Role,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static User ToUpdateStaffLesseeResponse(this UpdateStaffLesseeDTO updateStaffLesseeDTO, User existingUser)
        {
            if (updateStaffLesseeDTO == null) throw new ArgumentNullException(nameof(updateStaffLesseeDTO));
            if (existingUser == null) throw new ArgumentNullException(nameof(existingUser));

            existingUser.FullName = updateStaffLesseeDTO.FullName;
            existingUser.Address = updateStaffLesseeDTO.Address;
            existingUser.Email = updateStaffLesseeDTO.Email;
            existingUser.PhoneNumber = updateStaffLesseeDTO.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(updateStaffLesseeDTO.Password) && updateStaffLesseeDTO.Password != existingUser.Password)
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateStaffLesseeDTO.Password);
            }
            return existingUser;
        }
    }
}