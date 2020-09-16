using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// Class to manager DB operations for User
    /// </summary>
    public class UserDbManager : IUserDbManager
    {
        /// <summary>
        /// Method to authenticate user 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> AuthenticateUser(string userName, string password)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(d => d.Name == userName && d.Password == password);
                return user;
            }
        }

        /// <summary>
        /// Method to register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> RegisterUser(User user)
        {
            using (var context = new ToDoServiceDBContext())
            {
                context.Users.Add(user);
                return await context.SaveChangesAsync();
            }
        }
    }
}
