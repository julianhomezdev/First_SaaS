namespace SaaS.src.Application.DTOs.Product
{
    public class CreateProductRequest
    {


        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string Sizes { get; set; }

    }
}
