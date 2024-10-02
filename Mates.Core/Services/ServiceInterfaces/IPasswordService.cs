namespace Mates.Core.Services.ServiceInterfaces
{
    public interface IPasswordService
    {
        public string Hash(string password);
        public bool Verify(string password, string hashedPassowrd);
    }
}
