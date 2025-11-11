using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.DTOs.Report;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Infrastructure.Persistence;
using System.Runtime.CompilerServices;

namespace SaaS.src.Infrastructure.Data.Repositories.Invoice
{
    public class InvoiceRepository : IInvoiceRepository
    {


        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;

        }


        public async Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {

            var startDateTime = startDate.Date; // 00:00:00
            var endDateTime = endDate.Date.AddDays(1).AddTicks(-1);

            var saleDetails = await _context.InvoiceItems
                .Include(ii => ii.Invoice)
                .Where(ii => ii.Invoice.Date >= startDateTime && ii.Invoice.Date <= endDateTime)
                .Select(ii => new SalesDetailDto
                {

                    InvoiceNumber = ii.InvoiceId,
                    Date = ii.Invoice.Date,
                    CustomerName = ii.Invoice.CustomerName,
                    ProductName = _context.Products
                        .Where(p => p.Id == ii.ProductId)
                        .Select(p => p.ProductName)
                        .FirstOrDefault() ?? "Producto no encontrado",
                    Size = _context.Sizes
                        .Where(s => s.Id == ii.SizeId)
                        .Select(s => s.SizeName)
                        .FirstOrDefault() ?? "N/A",
                    Quantity = ii.Quantity,
                    UnitPrice = ii.UnitPrice,
                    Total = ii.Total


                })

                .OrderBy(sd => sd.Date)
                .ThenBy(sd => sd.InvoiceNumber)
                .ToListAsync();


            var grandTotal = saleDetails.Sum(sd => sd.Total);


            return new SalesReportDto
            {


                SaleDetails = saleDetails,
                GrandTotal = grandTotal

            };

        

        }
    }
}
