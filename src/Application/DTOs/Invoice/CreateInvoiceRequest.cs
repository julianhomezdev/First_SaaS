namespace SaaS.src.Application.DTOs.Invoice
{
    public class CreateInvoiceRequest
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
