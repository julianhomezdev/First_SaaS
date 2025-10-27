using MediatR;
using SaaS.src.Application.DTOs.Product;

namespace SaaS.src.Application.UseCases.Product.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponseDto>>
    {

        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public List<int>? SizeIds { get; set; }
    }
}
