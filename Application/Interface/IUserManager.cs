using Domain.Enum;
using System;
using System.Security.Claims;

namespace Application.Interface
{
    /// <summary>
    /// Interface for authenticating user and managing jwt token
    /// </summary>
    public interface IUserManager
    {
        string GenerateToken(Domain.Models.User user, string tokenKey);
        int GetUserId();
        UserType GetUserType();
        string GetUserName();
        Guid GetRequestId();
        ClaimsPrincipal GetUser();
    }
}
