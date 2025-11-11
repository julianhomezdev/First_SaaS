using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Product;

namespace SaaS.src.Application.UseCases.Product.Commands.Create
{
    public class CreateProductCommand : IRequest<Result<ProductResponseDto>>
    {

        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }

        public IFormFile?  ImageFile { get; set; }

        public List<ProductSizeRequest> Sizes { get; set; } = new List<ProductSizeRequest>();

    }
}
