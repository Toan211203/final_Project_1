using api.Data;
using api.DTOs.Book;
using api.DTOs.Rentals;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.RentalDetail
{
    public class RentalDetailRepository : IRentalDetailRepository
    {
        private readonly BookRentalContext _context;

        public RentalDetailRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalDetailDTO>> GetAllRentalDetailsAsync()
        {
            var rentalDetails = await _context.RentalDetails
                .Include(rd => rd.Book)
                .Include(rd => rd.Rental)
                .ToListAsync();

            return rentalDetails.Select(rd => new RentalDetailDTO
            {
                RentalDetailId = rd.RentalDetailId,
                RentalId = rd.RentalId,
                BookId = rd.BookId,
                Quantity = rd.Quantity,
                Book = new BookDTO
                {
                    BookId = rd.Book.BookId,
                    Title = rd.Book.Title,
                    Author = rd.Book.Author,
                    PublisherId = rd.Book.PublisherId,
                    Isbn = rd.Book.Isbn,
                    GenreId = rd.Book.GenreId,
                    PublishedYear = rd.Book.PublishedYear,
                    Quantity = rd.Book.Quantity,
                    Price = rd.Book.Price,
                    CreatedAt = rd.Book.CreatedAt
                },
                Rental = new RentalDTO
                {
                    RentalId = rd.Rental.RentalId,
                    UserId = rd.Rental.UserId,
                    RentalDate = rd.Rental.RentalDate,
                    DueDate = rd.Rental.DueDate,
                    ReturnDate = rd.Rental.ReturnDate,
                    Status = rd.Rental.Status,
                    TotalCost = rd.Rental.TotalCost
                }
            }).ToList();
        }

        public async Task<RentalDetailDTO?> GetRentalDetailByIdAsync(int id)
        {
            var rentalDetail = await _context.RentalDetails
                .Include(rd => rd.Book)
                .Where(rd => rd.BookId == rd.Book.BookId)
                .Include(rd => rd.Rental)
                .FirstOrDefaultAsync(rd => rd.RentalDetailId == id);

            if (rentalDetail == null) return null;

            return new RentalDetailDTO
            {
                RentalDetailId = rentalDetail.RentalDetailId,
                RentalId = rentalDetail.RentalId,
                BookId = rentalDetail.BookId,
                Quantity = rentalDetail.Quantity,
                BookTitle = rentalDetail.Book.Title,
                Book = new BookDTO
                {
                    BookId = rentalDetail.Book.BookId,
                    Title = rentalDetail.Book.Title,
                    Author = rentalDetail.Book.Author,
                    PublisherId = rentalDetail.Book.PublisherId,
                    Isbn = rentalDetail.Book.Isbn,
                    GenreId = rentalDetail.Book.GenreId,
                    PublishedYear = rentalDetail.Book.PublishedYear,
                    Quantity = rentalDetail.Book.Quantity,
                    Price = rentalDetail.Book.Price,
                    CreatedAt = rentalDetail.Book.CreatedAt
                },
                Rental = new RentalDTO
                {
                    RentalId = rentalDetail.Rental.RentalId,
                    UserId = rentalDetail.Rental.UserId,
                    RentalDate = rentalDetail.Rental.RentalDate,
                    DueDate = rentalDetail.Rental.DueDate,
                    ReturnDate = rentalDetail.Rental.ReturnDate,
                    Status = rentalDetail.Rental.Status,
                    TotalCost = rentalDetail.Rental.TotalCost
                }
            };
        }

        public async Task<RentalDetailDTO?> AddRentalDetailAsync(UpsertRentalDetailDTO upsertRentalDetailDTO)
        {
            var rentalDetail = upsertRentalDetailDTO.ToUpsertRentalDetailResponse();
            await _context.RentalDetails.AddAsync(rentalDetail);
            await _context.SaveChangesAsync();
            return rentalDetail.ToRentalDetailDTO();
        }

        public async Task<RentalDetailDTO?> UpdateRentalDetailAsync(int id, UpsertRentalDetailDTO upsertRentalDetailDTO)
        {
            var existingRentalDetail = await _context.RentalDetails.FindAsync(id);
            if (existingRentalDetail == null) return null;

            existingRentalDetail.RentalId = upsertRentalDetailDTO.RentalId;
            existingRentalDetail.BookId = upsertRentalDetailDTO.BookId;
            existingRentalDetail.Quantity = upsertRentalDetailDTO.Quantity;

            _context.RentalDetails.Update(existingRentalDetail);
            await _context.SaveChangesAsync();
            return existingRentalDetail.ToRentalDetailDTO();
        }

        public async Task<RentalDetailDTO?> DeleteRentalDetailAsync(int id)
        {
            var rentalDetail = await _context.RentalDetails.FindAsync(id);
            if (rentalDetail != null)
            {
                _context.RentalDetails.Remove(rentalDetail);
                await _context.SaveChangesAsync();
                return rentalDetail.ToRentalDetailDTO();
            }
            return null;
        }
    }
}