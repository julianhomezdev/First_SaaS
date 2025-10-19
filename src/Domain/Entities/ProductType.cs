namespace SaaS.src.Domain.Entities
{
    public class ProductType
    {

        public int Id { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }

        public ICollection<Product> Products { get; set; }  = new List<Product>();

    }
}
