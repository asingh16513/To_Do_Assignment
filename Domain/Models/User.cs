using Domain.Enum;

namespace Domain.Models
{
    /// <summary>
    /// Class to manage user
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// Requires password to be encrypted to Base64 for registering new user
        /// </summary>
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
