namespace SaaS.src.Application.DTOs.Product
{
    public class CreateProductDto
    {

        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }

        
       


    }
}
