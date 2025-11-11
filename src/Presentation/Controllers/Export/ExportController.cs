using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.UseCases.Report;

namespace SaaS.src.Presentation.Controllers.Export
{


    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {

        private readonly ExportSalesReportUseCase _exportSalesReportUseCase;


        public ExportController(ExportSalesReportUseCase exportSalesReportUseCase)
        {
            _exportSalesReportUseCase = exportSalesReportUseCase;
        }


        [HttpGet("export")]
        public async Task<IActionResult> ExportSalesReport(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var excelFile = await _exportSalesReportUseCase.ExecuteAsync(startDate, endDate);

                var fileName = $"ventas-{startDate:yyyy-MM-dd}-{endDate:yyyy-MM-dd}.xlsx";

                return File(
                    excelFile,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al generar el reporte", details = ex.Message });
            }
        }
    }
}

