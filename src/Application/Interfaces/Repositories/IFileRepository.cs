namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<string> SaveImageAsync(IFormFile imageFile);

    }
}
