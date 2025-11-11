namespace SaaS.src.Application.DTOs.Product
{
    public class CreateProductFormRequest
    {

        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }
        public string Sizes { get; set; }
        public IFormFile? ImageFile { get; set; } 

    }
}
