using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Infrastructure.Persistence;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Infrastructure.Data.Repositories.Users
{
    public class UserRepository: IUserRepository
    {

        private readonly AppDbContext _context;
        
        public UserRepository(AppDbContext context)
        {

            _context = context;

        }

        public async Task<User> GetByIdentificationAsync(string idNumber)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.IdentificationNumber == idNumber);




        }


        public async Task CreateAsync(User user)
        {


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }







    }
}
