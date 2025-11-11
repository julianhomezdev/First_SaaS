namespace SaaS.src.Infrastructure.Services
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using SaaS.src.Application.DTOs.Report;
    using SaaS.src.Application.Interfaces.Repositories;
    using System.Drawing;

    public class ExcelService : IExcelRepository
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public byte[] GenerateSalesReport(SalesReportDto reportData)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Reporte de Ventas");

            // Configurar encabezados
            SetupHeaders(worksheet);

            // Verificar si hay datos
            if (reportData.SaleDetails == null || reportData.SaleDetails.Count == 0)
            {
                // Agregar mensaje de "sin datos"
                worksheet.Cells["A2"].Value = "No hay datos en el rango de fechas seleccionado";
                worksheet.Cells["A2:I2"].Merge = true;
                worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2"].Style.Font.Italic = true;
                worksheet.Cells["A2"].Style.Font.Color.SetColor(Color.Gray);
            }
            else
            {
                // Llenar datos
                FillData(worksheet, reportData);

                // Agregar totales
                AddTotals(worksheet, reportData);

                // Aplicar estilos
                ApplyStyles(worksheet, reportData.SaleDetails.Count);
            }

            // Ajustar columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

        private void SetupHeaders(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1"].Value = "ID Factura";
            worksheet.Cells["B1"].Value = "Fecha";
            worksheet.Cells["C1"].Value = "Cliente";
            //worksheet.Cells["D1"].Value = "Documento";
            worksheet.Cells["E1"].Value = "Producto";
            worksheet.Cells["F1"].Value = "Talla";
            worksheet.Cells["G1"].Value = "Cantidad";
            worksheet.Cells["H1"].Value = "Precio Unitario";
            worksheet.Cells["I1"].Value = "Total";

            // Estilo del encabezado
            using var headerRange = worksheet.Cells["A1:I1"];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
            headerRange.Style.Font.Color.SetColor(Color.White);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void FillData(ExcelWorksheet worksheet, SalesReportDto reportData)
        {
            int row = 2;
            foreach (var detail in reportData.SaleDetails)
            {
                worksheet.Cells[row, 1].Value = detail.InvoiceNumber;
                worksheet.Cells[row, 2].Value = detail.Date.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 3].Value = detail.CustomerName;
                worksheet.Cells[row, 5].Value = detail.ProductName;
                worksheet.Cells[row, 6].Value = detail.Size;
                worksheet.Cells[row, 7].Value = detail.Quantity;
                worksheet.Cells[row, 8].Value = detail.UnitPrice;
                worksheet.Cells[row, 8].Style.Numberformat.Format = "$#,##0.00";
                worksheet.Cells[row, 9].Value = detail.Total;
                worksheet.Cells[row, 9].Style.Numberformat.Format = "$#,##0.00";

                row++;
            }
        }

        private void AddTotals(ExcelWorksheet worksheet, SalesReportDto reportData)
        {
            int totalRow = reportData.SaleDetails.Count + 2;

            worksheet.Cells[totalRow, 8].Value = "TOTAL GENERAL:";
            worksheet.Cells[totalRow, 8].Style.Font.Bold = true;
            worksheet.Cells[totalRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            worksheet.Cells[totalRow, 9].Value = reportData.GrandTotal;
            worksheet.Cells[totalRow, 9].Style.Font.Bold = true;
            worksheet.Cells[totalRow, 9].Style.Numberformat.Format = "$#,##0.00";
            worksheet.Cells[totalRow, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[totalRow, 9].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(146, 208, 80));
        }

        private void ApplyStyles(ExcelWorksheet worksheet, int dataRowCount)
        {
            // Bordes para toda la tabla (solo si hay datos)
            if (dataRowCount > 0)
            {
                var dataRange = worksheet.Cells[1, 1, dataRowCount + 1, 9];
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                // Alinear números a la derecha
                worksheet.Cells[2, 7, dataRowCount + 1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }
    }
}