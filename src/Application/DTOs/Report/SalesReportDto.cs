namespace SaaS.src.Application.DTOs.Report
{
    public class SalesReportDto
    {

        public List<SalesDetailDto> SaleDetails { get; set; } = new();
        public decimal GrandTotal { get; set; }

    }
}
