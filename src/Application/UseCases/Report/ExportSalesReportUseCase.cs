using SaaS.src.Application.Interfaces.Repositories;

namespace SaaS.src.Application.UseCases.Report
{
    public class ExportSalesReportUseCase
    {

        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IExcelRepository _excelRepository;


        public ExportSalesReportUseCase(
            
            IInvoiceRepository invoiceRepository,
            IExcelRepository excelRepository
            
            )
        {

            _invoiceRepository = invoiceRepository;
            _excelRepository = excelRepository;


        }



        public async Task<byte[]> ExecuteAsync(DateTime startDate, DateTime endDate)
        {

            if (startDate > endDate)
            {


                throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final");

            }

            var reportData = await _invoiceRepository.GetSalesReportAsync(startDate, endDate);


            var excelFile = _excelRepository.GenerateSalesReport(reportData);


            return excelFile;



        }

    }
}
