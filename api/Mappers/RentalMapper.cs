using System;
using System.Collections.Generic;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class RentalMappers
    {
        public static RentalDTO ToRentalDTO(this Rental rental)
        {
            if (rental == null) throw new ArgumentNullException(nameof(rental));

            return new RentalDTO
            {
                RentalId = rental.RentalId,
                UserId = rental.UserId,
                RentalDate = rental.RentalDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status,
                TotalCost = rental.TotalCost,
                Invoices = rental.Invoices?.Select(i => i.ToInvoiceDTO()).ToList() ?? new List<InvoiceDTO>(),
                RentalDetails = rental.RentalDetails?.Select(rd => rd.ToRentalDetailDTO()).ToList() ?? new List<RentalDetailDTO>(),
                User = rental.User?.ToStaffLesseDTO() ?? new StaffLesseDTO()
            };
        }

        public static Rental ToCreateRentalResponse(this CreateRentalDTO createRentalDTO)
        {
            if (createRentalDTO == null) throw new ArgumentNullException(nameof(createRentalDTO));

            return new Rental
            {
                UserId = createRentalDTO.UserId,
                //RentalDate = DateTime.UtcNow,
                //DueDate = DateTime.UtcNow.AddDays(7),
                TotalCost = createRentalDTO.TotalCost,
                Status = createRentalDTO.Status
            };
        }

        public static Rental ToUpdateRentalResponse(this UpdateRentalDTO updateRentalDTO, Rental existingRental)
        {
            if (updateRentalDTO == null) throw new ArgumentNullException(nameof(updateRentalDTO));
            if (existingRental == null) throw new ArgumentNullException(nameof(existingRental));

            existingRental.UserId = updateRentalDTO.UserId;
            existingRental.RentalDate = updateRentalDTO.RentalDate;
            existingRental.DueDate = updateRentalDTO.DueDate;
            existingRental.ReturnDate = updateRentalDTO.ReturnDate;
            existingRental.TotalCost = updateRentalDTO.TotalCost;
            existingRental.Status = updateRentalDTO.Status;

            return existingRental;
        }
    }
}