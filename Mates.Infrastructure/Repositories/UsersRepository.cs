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
            _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
