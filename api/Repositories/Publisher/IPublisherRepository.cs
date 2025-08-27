using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Publisher;

namespace api.Repositories.Publisher
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<PublisherDTO>> GetAllPublishersAsync();
        Task<PublisherDTO?> GetPublisherByIdAsync(int id);
        Task<PublisherDTO?> AddPublisherAsync(CreatePublisherDTO createPublisherDTO);
        Task<PublisherDTO?> UpdatePublisherAsync(int id, UpdatePublisherDTO updatePublisherDTO);
        Task<PublisherDTO?> DeletePublisherAsync(int id);
    }
}