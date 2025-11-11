using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetByIdentificationAsync(string idNumber);
        Task CreateAsync(User user);
    }
}
