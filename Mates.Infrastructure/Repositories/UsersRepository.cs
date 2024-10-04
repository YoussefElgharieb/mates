


using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Mates.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository (ApplicationDbContext context)
        {
            _context = context?? throw new ArgumentNullException("'context' cannot be null");
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUser(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }


    }
}
