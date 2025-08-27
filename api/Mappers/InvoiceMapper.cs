using System;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.DTOs.Users;
using api.Models;

namespace api.Mappers
{
    public static class InvoiceMappers
    {
        public static InvoiceDTO ToInvoiceDTO(this Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            return new InvoiceDTO
            {
                InvoiceId = invoice.InvoiceId,
                RentalId = invoice.RentalId,
                UserId = invoice.UserId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                PaymentStatus = invoice.PaymentStatus,
                Rental = invoice.Rental?.ToRentalDTO() ?? new RentalDTO(),
                User = invoice.User?.ToStaffLesseDTO() ?? new StaffLesseDTO()
            };
        }

        public static Invoice ToCreateInvoiceResponse(this CreateInvoiceDTO createInvoiceDTO)
        {
            if (createInvoiceDTO == null) throw new ArgumentNullException(nameof(createInvoiceDTO));

            return new Invoice
            {
                RentalId = createInvoiceDTO.RentalId,
                UserId = createInvoiceDTO.UserId,
                TotalAmount = createInvoiceDTO.TotalAmount,
                PaymentStatus = createInvoiceDTO.PaymentStatus,
                InvoiceDate = DateTime.UtcNow
            };
        }

        public static Invoice ToUpdateInvoiceResponse(this UpdateInvoiceDTO updateInvoiceDTO, Invoice existingInvoice)
        {
            if (updateInvoiceDTO == null) throw new ArgumentNullException(nameof(updateInvoiceDTO));
            if (existingInvoice == null) throw new ArgumentNullException(nameof(existingInvoice));

            existingInvoice.RentalId = updateInvoiceDTO.RentalId;
            existingInvoice.UserId = updateInvoiceDTO.UserId;
            existingInvoice.TotalAmount = updateInvoiceDTO.TotalAmount;
            existingInvoice.PaymentStatus = updateInvoiceDTO.PaymentStatus;

            return existingInvoice;
        }
    }
}