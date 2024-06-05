using Microsoft.Azure.Cosmos;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IUserInterface
    {
        Task<User> UserRegister(User user);

        Task<string> LoginUser(User user);

    }
}
