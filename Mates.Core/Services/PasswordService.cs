using Mates.Core.Services.ServiceInterfaces;
using BC = BCrypt.Net.BCrypt;

namespace Mates.Core.Services
{
    public class PasswordService : IPasswordService
    {
        public string Hash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BC.Verify(password, hashedPassword);
        }
    }
}
