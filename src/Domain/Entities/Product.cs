using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace SaaS.src.Domain.Entities
{
    public class Product
    {


        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }

        // Relation with producttype
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }


        public ICollection<ProductsSizes> ProductSizes { get; set; } = new List<ProductsSizes>();

        // General Quantity (not map)
        public int ProductTotalQuantity => ProductSizes?.Sum(ps => ps.SizeStock) ?? 0;

        public string? ImageUrl { get; set; }

    }
}
