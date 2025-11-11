using MediatR;
using SaaS.src.Application.Common;

namespace SaaS.src.Application.UseCases.Product.Commands.Delete
{
    public class DeleteProductCommand : IRequest<Result<bool>>
    {
        public int ProductId { get; set; }


    }
}
