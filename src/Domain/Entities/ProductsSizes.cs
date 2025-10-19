namespace SaaS.src.Domain.Entities
{
    public class ProductsSizes
    {


        public int Id { get; set; }

        // Fk to Product
        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation

        // Fk to Size
        public int SizeId { get; set; }
        public Size Size { get; set; } // Navigation


        // Quantity per size
        public int SizeStock { get; set; }

      

    }
}
