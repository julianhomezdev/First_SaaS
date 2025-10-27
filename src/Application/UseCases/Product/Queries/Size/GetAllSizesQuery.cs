using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Size;

namespace SaaS.src.Application.UseCases.Product.Queries.Size
{
    public class GetAllSizesQuery : IRequest<Result<List<SizeDto>>>
    {



    }
}
