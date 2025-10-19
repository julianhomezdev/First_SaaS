namespace SaaS.src.Application.DTOs.Product
{
    public class UpdateProductDto
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }
    }
}
