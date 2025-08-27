using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.Invoice;

namespace api.Repositories.Invoice
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<InvoiceDTO>> GetAllInvoicesAsync();
        Task<InvoiceDTO?> GetInvoiceByIdAsync(int id);
        Task<InvoiceDTO?> AddInvoiceAsync(CreateInvoiceDTO createInvoiceDTO);
        Task<InvoiceDTO?> UpdateInvoiceAsync(int id, UpdateInvoiceDTO updateInvoiceDTO);
        Task<InvoiceDTO?> DeleteInvoiceAsync(int id);
        Task<IEnumerable<InvoiceDTO>> GetInvoicesByUserIdAsync(int id);

    }
}