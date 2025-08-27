using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Publisher;
using api.Repositories.Publisher;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepo;

        public PublisherController(IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetPublishers()
        {
            var publishers = await _publisherRepo.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPublisherById([FromRoute] int id)
        {
            var publisher = await _publisherRepo.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<ActionResult> AddPublisher(CreatePublisherDTO createPublisherDTO)
        {
            var publisher = await _publisherRepo.AddPublisherAsync(createPublisherDTO);
            return CreatedAtAction(nameof(GetPublisherById), new { id = publisher?.PublisherId }, publisher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisher(int id, UpdatePublisherDTO updatePublisherDTO)
        {
            var publisher = await _publisherRepo.UpdatePublisherAsync(id, updatePublisherDTO);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            var publisher = await _publisherRepo.DeletePublisherAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }
    }
}