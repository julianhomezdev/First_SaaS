namespace SaaS.src.Domain.Entities
{
    public class Size
    {

        public int Id { get; set; }
        public string? SizeName { get; set; }

        public ICollection<ProductsSizes> ProductSizes { get; set; } = new List<ProductsSizes>();
    }
}
