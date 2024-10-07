using Mates.Core.Domain.Entities;

namespace Mates.Core.Domain.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User?> GetUser(Guid Id);
        public Task<User?> GetUserAsync(String Email);
    }
}
