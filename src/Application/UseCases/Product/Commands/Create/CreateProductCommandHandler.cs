using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.Interfaces.Repositories;
using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.UseCases.Product.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResponseDto>>
    {

        private readonly IProductRepository _productRepository;


        //Constructor: inyect repository to save the bd
        public CreateProductCommandHandler(IProductRepository productRepository) {

            _productRepository = productRepository;
        
        
        }


        // Executes the creation logic
        public async Task<Result<ProductResponseDto>> Handle(
            CreateProductCommand  request,
            CancellationToken cancellationToken)
        {

            // Create the product object 
            var product = new Domain.Entities.Product
            {
                ProductName = request.ProductName,
                ProductReference = request.ProductReference,
                ProductPrice = request.ProductPrice,
                ProductTypeId = request.ProductTypeId

            };

            foreach( var sizeRequest in request.Sizes)
            {

                product.ProductSizes.Add(new ProductsSizes
                {

                    SizeId = sizeRequest.SizeId,
                    SizeStock = sizeRequest.Stock

                });

            }

            // Save into bd
            var createdProduct = await _productRepository.CreateProductAsync(product);

            // Create the response
            var response = new ProductResponseDto
            {

                Id = createdProduct.Id,
                ProductName = createdProduct.ProductName,
                ProductReference = createdProduct.ProductReference,
                ProductPrice = createdProduct.ProductPrice,



            };

            // Return the result
            return Result<ProductResponseDto>.Success(

                response,
                "Successfully created product"
                );

        }

    }
}
