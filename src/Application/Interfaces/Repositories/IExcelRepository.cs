using SaaS.src.Application.DTOs.Report;

namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface IExcelRepository
    {


        byte[] GenerateSalesReport(SalesReportDto reportData);

    }
}
