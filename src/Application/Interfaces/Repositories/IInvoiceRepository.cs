using SaaS.src.Application.DTOs.Report;

namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {

        Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate);

    }
}
