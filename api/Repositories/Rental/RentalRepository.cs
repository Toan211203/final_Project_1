
using api.Data;
using api.DTOs.Book;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Rental
{
    public class RentalRepository : IRentalRepository
    {
        private readonly BookRentalContext _context;

        public RentalRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalDTO>> GetAllRentalsAsync()
        {
            var rentals = await _context.Rentals
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.Book)
                .Include(r => r.Invoices)
                .Include(r => r.User)
                .ToListAsync();

            return rentals.Select(r => new RentalDTO
            {
                RentalId = r.RentalId,
                UserId = r.UserId,
                RentalDate = r.RentalDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate,
                Status = r.Status,
                TotalCost = r.TotalCost,
                Invoices = r.Invoices?.Select(i => new InvoiceDTO
                {
                    InvoiceId = i.InvoiceId,
                    RentalId = i.RentalId,
                    UserId = i.UserId,
                    InvoiceDate = i.InvoiceDate,
                    TotalAmount = i.TotalAmount,
                    PaymentStatus = i.PaymentStatus,
                }).ToList() ?? new List<InvoiceDTO>(),
                RentalDetails = r.RentalDetails?.Select(rd => new RentalDetailDTO
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
                    }
                }).ToList()
            }).ToList();
        }

        public async Task<RentalDTO?> GetRentalByIdAsync(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .Include(r => r.Invoices)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null) return null;

            return new RentalDTO
            {
                RentalId = rental.RentalId,
                UserId = rental.UserId,
                RentalDate = rental.RentalDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status,
                TotalCost = rental.TotalCost,
                Invoices = rental.Invoices.Select(i => new InvoiceDTO
                {
                    InvoiceId = i.InvoiceId,
                    RentalId = i.RentalId,
                    UserId = i.UserId,
                    InvoiceDate = i.InvoiceDate,
                    TotalAmount = i.TotalAmount,
                    PaymentStatus = i.PaymentStatus,
                }).ToList(),
                RentalDetails = rental.RentalDetails.Select(rd => new RentalDetailDTO
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
                    }
                }).ToList()
            };
        }

        public async Task<RentalDTO?> AddRentalAsync(CreateRentalDTO createRentalDTO)
        {
            var rental = new api.Models.Rental
            {
                UserId = createRentalDTO.UserId,
                RentalDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = 0,
                TotalCost = 0
            };

            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            //await AddRentalDetailsFromCartAsync(rental.RentalId, createRentalDTO.UserId);
            //await CalculateTotalCostAsync(rental.RentalId);

            var cartItems = await _context.Carts
                .Where(c => c.UserId == createRentalDTO.UserId)
                .Include(c => c.Book)
                .ToListAsync();
            foreach (var cartItem in cartItems)
            {
                var rentalDetail = new api.Models.RentalDetail
                {
                    RentalId = rental.RentalId,
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity
                };

                await _context.RentalDetails.AddAsync(rentalDetail);
            }
            await _context.SaveChangesAsync();
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            await CalculateTotalCostAsync(rental.RentalId);

            var invoice = new api.Models.Invoice
            {
                UserId = rental.UserId,
                RentalId = rental.RentalId,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = rental.TotalCost,
                PaymentStatus = 0
            };
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            var updatedRental = await _context.Rentals.FindAsync(rental.RentalId);
            return updatedRental?.ToRentalDTO();
        }

        private async Task AddRentalDetailsFromCartAsync(int rentalId, int userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Book)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return;
            }

            foreach (var cartItem in cartItems)
            {
                var rentalDetail = new api.Models.RentalDetail
                {
                    RentalId = rentalId,
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity
                };

                await _context.RentalDetails.AddAsync(rentalDetail);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<decimal> CalculateTotalCostAsync(int rentalId)
        {
            var totalCost = await _context.RentalDetails
                .Where(rd => rd.RentalId == rentalId)
                .SumAsync(rd => rd.Book.Price * rd.Quantity);

            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.TotalCost = totalCost;
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();
            }

            return totalCost;
        }

        public async Task<RentalDTO?> UpdateRentalAsync(int id, UpdateRentalDTO updateRentalDTO)
        {
            var existingRental = await _context.Rentals.FindAsync(id);
            if (existingRental == null) return null;

            existingRental.UserId = updateRentalDTO.UserId;
            existingRental.RentalDate = updateRentalDTO.RentalDate;
            existingRental.DueDate = updateRentalDTO.DueDate;
            existingRental.ReturnDate = updateRentalDTO.ReturnDate;
            existingRental.TotalCost = updateRentalDTO.TotalCost;
            existingRental.Status = updateRentalDTO.Status;

            _context.Rentals.Update(existingRental);
            await _context.SaveChangesAsync();
            return existingRental.ToRentalDTO();
        }

        public async Task<RentalDTO?> DeleteRentalAsync(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
                return rental.ToRentalDTO();
            }
            return null;
        }

        public async Task<IEnumerable<RentalDTO>> GetRentalsByUserIdAsync(int userId)
        {
            var rentals = await _context.Rentals
                .Where(r => r.UserId == userId)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.Book)
                .Include(r => r.Invoices)
                .Include(r => r.User)
                .ToListAsync();

            return rentals.Select(rental => new RentalDTO
            {
                RentalId = rental.RentalId,
                UserId = rental.UserId,
                RentalDate = rental.RentalDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status,
                TotalCost = rental.TotalCost,
                Invoices = rental.Invoices.Select(i => new InvoiceDTO
                {
                    InvoiceId = i.InvoiceId,
                    RentalId = i.RentalId,
                    UserId = i.UserId,
                    InvoiceDate = i.InvoiceDate,
                    TotalAmount = i.TotalAmount,
                    PaymentStatus = i.PaymentStatus,
                }).ToList(),
                RentalDetails = rental.RentalDetails.Select(rd => new RentalDetailDTO
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
                    }
                }).ToList()
            }).ToList();
        }
        
    }
}