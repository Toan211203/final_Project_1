using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Invoice;
using api.Repositories.Invoice;
using Microsoft.AspNetCore.Authorization;
using api.Repositories.RentalDetail;
using api.Repositories.Rental;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IRentalRepository _rentalRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IRentalRepository rentalRepository)
        {
            _invoiceRepository = invoiceRepository;
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            var sortedInvoices = invoices.OrderByDescending(i => i.InvoiceDate).ToList();
            return Ok(sortedInvoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById([FromRoute] int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDTO createInvoiceDTO)
        {
            // var userIdClaim = User.FindFirst("UserId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User not authenticated.");
            // }
            // var userId = int.Parse(userIdClaim.Value);
            // createInvoiceDTO.UserId = userId;
            var invoice = await _invoiceRepository.AddInvoiceAsync(createInvoiceDTO);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice?.InvoiceId }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] int id, [FromBody] UpdateInvoiceDTO updateInvoiceDTO)
        {
            var invoice = await _invoiceRepository.UpdateInvoiceAsync(id, updateInvoiceDTO);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id)
        {
            var invoice = await _invoiceRepository.DeleteInvoiceAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetInvoicesByUserId([FromRoute] int userId)
        {
            var invoices = await _invoiceRepository.GetInvoicesByUserIdAsync(userId);
            if (invoices == null || !invoices.Any())
            {
                return NotFound();
            }
            return Ok(invoices);
        }

    }
}
