using MediatR;
using Microsoft.AspNetCore.Http;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Product;

namespace SaaS.src.Application.UseCases.Product.Commands.Update
{
    public class UpdateProductCommand : IRequest<Result<bool>>
    {

         public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductTypeId { get; set; }
        public List<ProductSizeRequest> Sizes { get; set; }
        public IFormFile ImageFile { get; set; }

}
}
