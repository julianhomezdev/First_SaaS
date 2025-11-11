using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Common;
using SaaS.src.Infrastructure.Persistence;
using SaaS.src.Application.DTOs.Invoice;

namespace SaaS.src.Application.UseCases.Invoice.Commands.Create
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<InvoiceResponse>>
    {
        private readonly AppDbContext _context;

        public CreateInvoiceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<InvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var productSize = await _context.ProductsSizes
                    .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.SizeId == item.SizeId, cancellationToken);

                if (productSize == null)
                {
                    return Result<InvoiceResponse>.Failure("Producto o talla no encontrada");
                }

                if (productSize.SizeStock < item.Quantity)
                {
                    return Result<InvoiceResponse>.Failure($"Stock insuficiente para producto {item.ProductId}");
                }

                productSize.SizeStock -= item.Quantity;
            }

            var invoice = new Domain.Entities.Invoice
            {
                InvoiceNumber = request.InvoiceNumber,
                CustomerName = request.CustomerName,
                CustomerDocument = request.CustomerDocument,
                Date = request.Date,
                Subtotal = request.Subtotal,
                Tax = request.Tax,
                Total = request.Total,
                Items = request.Items.Select(i => new Domain.Entities.InvoiceItem
                {
                    ProductId = i.ProductId,
                    SizeId = i.SizeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.Total
                }).ToList()
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new InvoiceResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                CustomerDocument = invoice.CustomerDocument,
                Date = invoice.Date,
                Subtotal = invoice.Subtotal,
                Tax = invoice.Tax,
                Total = invoice.Total
            };

            return Result<InvoiceResponse>.Success(response);
        }
    }
}