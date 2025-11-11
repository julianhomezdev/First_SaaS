using MediatR;
using Microsoft.VisualBasic;
using SaaS.src.Application.Common;
using SaaS.src.Application.Interfaces.Repositories;

namespace SaaS.src.Application.UseCases.Product.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository; ;
        }

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            
            try
            {

                var product = await _productRepository.GetByIdAsync(request.ProductId);

                if (product == null)
                {

                    return Result<bool>.Failure($"Product with id {request.ProductId} not found");

                }


                await _productRepository.DeleteAsync(request.ProductId);


                return Result<bool>.Success(true);

            }

            catch (Exception ex)
            {

                return Result<bool>.Failure($"Error deleting the product: {ex.Message}");

            }

        }
    }
}
