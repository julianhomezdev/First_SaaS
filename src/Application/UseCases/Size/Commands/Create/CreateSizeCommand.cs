using MediatR;

namespace SaaS.src.Application.UseCases.Size.Commands.Create
{
    public class CreateSizeCommand : IRequest<int>
    {
        public string SizeName { get; set; } = string.Empty;

    }
}
