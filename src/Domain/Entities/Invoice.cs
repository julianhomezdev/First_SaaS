namespace SaaS.src.Domain.Entities
{
    public class Invoice
    {


        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public DateTime Date { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }


}
