using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Book;
using api.DTOs.Publisher;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Publisher
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookRentalContext _context;

        public PublisherRepository(BookRentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PublisherDTO>> GetAllPublishersAsync()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .ToListAsync();

            return publishers.Select(publisher => new PublisherDTO
            {
                PublisherId = publisher.PublisherId,
                PublisherName = publisher.PublisherName,
                ContactPerson = publisher.ContactPerson,
                PhoneNumber = publisher.PhoneNumber,
                Email = publisher.Email,
                Books = publisher.Books.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author
                }).ToList()
            }).ToList();
        }

        public async Task<PublisherDTO?> GetPublisherByIdAsync(int publisherId)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.PublisherId == publisherId);

            if (publisher == null) return null;

            return new PublisherDTO
            {
                PublisherId = publisher.PublisherId,
                PublisherName = publisher.PublisherName,
                ContactPerson = publisher.ContactPerson,
                PhoneNumber = publisher.PhoneNumber,
                Email = publisher.Email,
                CreatedAt = publisher.CreatedAt,
                Books = publisher.Books.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    PublisherId = b.PublisherId,
                    Isbn = b.Isbn,
                    GenreId = b.GenreId,
                    PublishedYear = b.PublishedYear,
                    Quantity = b.Quantity,
                    Price = b.Price,
                    CreatedAt = b.CreatedAt
                }).ToList()
            };
        }

        public async Task<PublisherDTO?> AddPublisherAsync(CreatePublisherDTO createPublisherDTO)
        {
            var publisher = createPublisherDTO.ToCreatePublisherResponse();
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher.ToPublisherDTO();
        }

        public async Task<PublisherDTO?> UpdatePublisherAsync(int id, UpdatePublisherDTO updatePublisherDTO)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher == null) return null;

            if (!string.IsNullOrEmpty(updatePublisherDTO.PublisherName))
                existingPublisher.PublisherName = updatePublisherDTO.PublisherName;

            if (!string.IsNullOrEmpty(updatePublisherDTO.ContactPerson))
                existingPublisher.ContactPerson = updatePublisherDTO.ContactPerson;

            if (!string.IsNullOrEmpty(updatePublisherDTO.PhoneNumber))
                existingPublisher.PhoneNumber = updatePublisherDTO.PhoneNumber;

            if (!string.IsNullOrEmpty(updatePublisherDTO.Email))
                existingPublisher.Email = updatePublisherDTO.Email;

            _context.Publishers.Update(existingPublisher);
            await _context.SaveChangesAsync();
            return existingPublisher.ToPublisherDTO();
        }

        public async Task<PublisherDTO?> DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
                return publisher.ToPublisherDTO();
            }
            return null;
        }
    }
}