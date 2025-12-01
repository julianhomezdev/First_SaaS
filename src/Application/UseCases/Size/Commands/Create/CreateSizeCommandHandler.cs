using MediatR;
using SaaS.src.Application.Interfaces.Repositories;

namespace SaaS.src.Application.UseCases.Size.Commands.Create
{
    public class CreateSizeCommandHandler : IRequestHandler<CreateSizeCommand, int>
    {

        private readonly ISizeRepository _sizeRepository;

        public CreateSizeCommandHandler(ISizeRepository sizeRepository) { 
        
            _sizeRepository = sizeRepository;
        
        
        }


        public async Task<int> Handle(CreateSizeCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.SizeName))
            {

                throw new ArgumentException("El nombre del size es requerido");

            }


            var size = new Domain.Entities.Size
            {

                SizeName = request.SizeName.Trim()


            };

            var createdSize = await _sizeRepository.CreateSizeAsync(size);


            return createdSize.Id;


        }

    }
}
