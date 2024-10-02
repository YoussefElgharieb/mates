using Mates.Core.Domain.Entities;

namespace Mates.Core.Domain.RepositoryContracts
{
    public interface IUsersRepository
    {
        public Task<User?> CreateUser(User user);

        public Task<User?> GetUser(String Email);
    }
}
