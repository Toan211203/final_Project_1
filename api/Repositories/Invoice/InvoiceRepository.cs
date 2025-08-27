using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Invoice
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly BookRentalContext _context;

        public InvoiceRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllInvoicesAsync()
        {
             var invoices = await _context.Invoices
                .Include(i => i.Rental)
                    .ThenInclude(r => r.RentalDetails)
                        .ThenInclude(rd => rd.Book)
                .Include(i => i.User)
                .ToListAsync();

            return invoices.Select(i => new InvoiceDTO
            {
                InvoiceId = i.InvoiceId,
                RentalId = i.RentalId,
                UserId = i.UserId,
                InvoiceDate = i.InvoiceDate,
                TotalAmount = i.TotalAmount,
                PaymentStatus = i.PaymentStatus,
                Rental = new RentalDTO
                {
                    RentalId = i.Rental.RentalId,
                    UserId = i.Rental.UserId,
                    RentalDate = i.Rental.RentalDate,
                    DueDate = i.Rental.DueDate,
                    ReturnDate = i.Rental.ReturnDate,
                    Status = i.Rental.Status,
                    TotalCost = i.Rental.TotalCost,
                    RentalDetails = i.Rental.RentalDetails?.Select(rd => new RentalDetailDTO
                    {
                        RentalDetailId = rd.RentalDetailId,
                        RentalId = rd.RentalId,
                        BookId = rd.BookId,
                        Quantity = rd.Quantity,
                        BookTitle = rd.Book.Title
                    }).ToList() ?? new List<RentalDetailDTO>()
                }
            }).ToList();
        }

        public async Task<InvoiceDTO?> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Rental)
                    .ThenInclude(r => r.RentalDetails)
                        .ThenInclude(rd => rd.Book)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null) return null;

            return new InvoiceDTO
            {
                InvoiceId = invoice.InvoiceId,
                RentalId = invoice.RentalId,
                UserId = invoice.UserId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                PaymentStatus = invoice.PaymentStatus,
                Rental = invoice.Rental != null ? new RentalDTO
                {
                    RentalId = invoice.Rental.RentalId,
                    UserId = invoice.Rental.UserId,
                    RentalDate = invoice.Rental.RentalDate,
                    DueDate = invoice.Rental.DueDate,
                    ReturnDate = invoice.Rental.ReturnDate,
                    Status = invoice.Rental.Status,
                    TotalCost = invoice.Rental.TotalCost,
                    RentalDetails = invoice.Rental.RentalDetails?.Select(rd => new RentalDetailDTO
                    {
                        RentalDetailId = rd.RentalDetailId,
                        RentalId = rd.RentalId,
                        BookId = rd.BookId,
                        Quantity = rd.Quantity,
                        BookTitle = rd.Book.Title,
                    }).ToList() ?? new List<RentalDetailDTO>()
                } : null
            };
        }

        public async Task<InvoiceDTO?> AddInvoiceAsync(CreateInvoiceDTO createInvoiceDTO)
        {
            var invoice = createInvoiceDTO.ToCreateInvoiceResponse();
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice.ToInvoiceDTO();
        }

        public async Task<InvoiceDTO?> UpdateInvoiceAsync(int id, UpdateInvoiceDTO updateInvoiceDTO)
        {
            var existingInvoice = await _context.Invoices.FindAsync(id);
            if (existingInvoice == null) return null;

            existingInvoice.RentalId = updateInvoiceDTO.RentalId;
            existingInvoice.UserId = updateInvoiceDTO.UserId;
            existingInvoice.TotalAmount = updateInvoiceDTO.TotalAmount;
            existingInvoice.PaymentStatus = updateInvoiceDTO.PaymentStatus;

            _context.Invoices.Update(existingInvoice);
            await _context.SaveChangesAsync();
            return existingInvoice.ToInvoiceDTO();
        }

        public async Task<InvoiceDTO?> DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
                return invoice.ToInvoiceDTO();
            }
            return null;
        }

       public async Task<IEnumerable<InvoiceDTO>> GetInvoicesByUserIdAsync(int userId)
        {
            var invoices = await _context.Invoices
                .Where(i => i.UserId == userId)
                .Include(i => i.Rental)
                    .ThenInclude(r => r.RentalDetails)
                        .ThenInclude(rd => rd.Book)
                .Include(i => i.User)
                .ToListAsync();

            return invoices.Select(invoice => new InvoiceDTO
            {
                InvoiceId = invoice.InvoiceId,
                RentalId = invoice.RentalId,
                UserId = invoice.UserId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                PaymentStatus = invoice.PaymentStatus,
                Rental = invoice.Rental != null ? new RentalDTO
                {
                    RentalId = invoice.Rental.RentalId,
                    UserId = invoice.Rental.UserId,
                    RentalDate = invoice.Rental.RentalDate,
                    DueDate = invoice.Rental.DueDate,
                    ReturnDate = invoice.Rental.ReturnDate,
                    Status = invoice.Rental.Status,
                    TotalCost = invoice.Rental.TotalCost,
                    RentalDetails = invoice.Rental.RentalDetails?.Select(rd => new RentalDetailDTO
                    {
                        RentalDetailId = rd.RentalDetailId,
                        RentalId = rd.RentalId,
                        BookId = rd.BookId,
                        Quantity = rd.Quantity,
                        BookTitle = rd.Book.Title,
                    }).ToList() ?? new List<RentalDetailDTO>()
                } : null
            }).ToList();
        }
    }
}

/*
RentalDetails = invoice.Rental.RentalDetails?.Select(rd => new RentalDetailDTO
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
                    }).ToList() ?? new List<RentalDetailDTO>()
*/