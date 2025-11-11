namespace SaaS.src.Domain.Entities
{
    public class InvoiceItem
    {

        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public Invoice Invoice { get; set; }
    }
}
