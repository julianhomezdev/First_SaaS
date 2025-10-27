using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.Interfaces.Repositories;
using System.Runtime.CompilerServices;
using AutoMapper;



namespace SaaS.src.Application.UseCases.Product.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(request.SizeIds);

            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                products = products
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value);
            }

            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }
    }

        

}
