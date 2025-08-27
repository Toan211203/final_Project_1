using System;
using System.Collections.Generic;
using api.DTOs.Book;
using api.DTOs.Publisher;
using api.Models;

namespace api.Mappers
{
    public static class PublisherMappers
    {
        public static PublisherDTO ToPublisherDTO(this Publisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));

            return new PublisherDTO
            {
                PublisherId = publisher.PublisherId,
                PublisherName = publisher.PublisherName,
                ContactPerson = publisher.ContactPerson,
                PhoneNumber = publisher.PhoneNumber,
                Email = publisher.Email,
                CreatedAt = publisher.CreatedAt,
                Books = publisher.Books?.Select(b => b.ToBookDTO()).ToList() ?? new List<BookDTO>()
            };
        }

        public static Publisher ToCreatePublisherResponse(this CreatePublisherDTO createPublisherDTO)
        {
            if (createPublisherDTO == null) throw new ArgumentNullException(nameof(createPublisherDTO));

            return new Publisher
            {
                PublisherName = createPublisherDTO.PublisherName,
                ContactPerson = createPublisherDTO.ContactPerson,
                PhoneNumber = createPublisherDTO.PhoneNumber,
                Email = createPublisherDTO.Email,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Publisher ToUpdatePublisherResponse(this UpdatePublisherDTO updatePublisherDTO, Publisher existingPublisher)
        {
            if (updatePublisherDTO == null) throw new ArgumentNullException(nameof(updatePublisherDTO));
            if (existingPublisher == null) throw new ArgumentNullException(nameof(existingPublisher));

            existingPublisher.PublisherName = updatePublisherDTO.PublisherName;
            existingPublisher.ContactPerson = updatePublisherDTO.ContactPerson;
            existingPublisher.PhoneNumber = updatePublisherDTO.PhoneNumber;
            existingPublisher.Email = updatePublisherDTO.Email;

            return existingPublisher;
        }
    }
}