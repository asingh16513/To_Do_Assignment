using Domain.Models;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// Interface for db operations for User
    /// </summary>
    public interface IUserDbManager
    {
        Task<User> AuthenticateUser(string userName, string password);
        Task<int> RegisterUser(User user);

    }
}
