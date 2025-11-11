namespace SaaS.src.Application.DTOs.Invoice
{
    public class InvoiceItemRequest
    {

        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }


    }
}
