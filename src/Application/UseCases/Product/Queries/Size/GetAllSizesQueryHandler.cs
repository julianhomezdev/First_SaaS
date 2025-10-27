using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.DTOs.Size;
using SaaS.src.Application.Interfaces.Repositories;

namespace SaaS.src.Application.UseCases.Product.Queries.Size
{
    public class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, Result<List<SizeDto>>>
    {
        private readonly ISizeRepository _sizeRepository;

        public GetAllSizesQueryHandler(ISizeRepository sizeRepository)
        {

            _sizeRepository = sizeRepository;

        }

        public async Task<Result<List<SizeDto>>> Handle(GetAllSizesQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var sizes = await _sizeRepository.GetAllSizesAsync();

                var sizeDtos = sizes.Select(s => new SizeDto
                {
                    Id = s.Id,
                    SizeName = s.SizeName
                }).ToList();

                return Result<List<SizeDto>>.Success(sizeDtos);
            }

            catch (Exception ex)

            {
                return Result<List<SizeDto>>.Failure($"Error retrieving sizes: {ex.Message}");
            }

        }
            

    }
}
