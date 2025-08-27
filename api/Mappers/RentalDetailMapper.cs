using System;
using api.DTOs.Book;
using api.DTOs.Rentals;
using api.Models;

namespace api.Mappers
{
    public static class RentalDetailMappers
    {
        public static RentalDetailDTO ToRentalDetailDTO(this RentalDetail rentalDetail)
        {
            if (rentalDetail == null) throw new ArgumentNullException(nameof(rentalDetail));

            return new RentalDetailDTO
            {
                RentalDetailId = rentalDetail.RentalDetailId,
                RentalId = rentalDetail.RentalId,
                BookId = rentalDetail.BookId,
                Quantity = rentalDetail.Quantity,
                Book = rentalDetail.Book?.ToBookDTO() ?? new BookDTO(),
                Rental = rentalDetail.Rental?.ToRentalDTO() ?? new RentalDTO()
            };
        }

        public static RentalDetail ToUpsertRentalDetailResponse(this UpsertRentalDetailDTO upsertRentalDetailDTO)
        {
            if (upsertRentalDetailDTO == null) throw new ArgumentNullException(nameof(upsertRentalDetailDTO));

            return new RentalDetail
            {
                RentalId = upsertRentalDetailDTO.RentalId,
                BookId = upsertRentalDetailDTO.BookId,
                Quantity = upsertRentalDetailDTO.Quantity
            };
        }
    }
}