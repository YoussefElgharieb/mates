using Mates.Core.Domain.Entities;

namespace Mates.Core.Domain.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByEmailAsync(String email);
        public Task<List<User>> GetAllUsersAsync();
    }
}
