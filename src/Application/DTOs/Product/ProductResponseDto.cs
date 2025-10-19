namespace SaaS.src.Application.DTOs.Product
{
    public class ProductResponseDto
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTotalQuantity { get; set; }
    }
}
