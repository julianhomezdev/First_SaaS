using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Invoice;

namespace SaaS.src.Application.UseCases.Invoice.Commands.Create
{
    public class CreateInvoiceCommand : IRequest<Result<InvoiceResponse>>
    {

        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItemRequest> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }



    }
}
