using Application.Interface;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Helper
{
    /// <summary>
    /// Class to manage user authentication and generate jwt token
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor _accessor;
        private const string _userId = "userId";
        private const string _userType = "userType";
        private const string _userName = "userName";
        private const string _requestId = "requestId";

        public UserManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public ClaimsPrincipal GetUser()
        {
            return _accessor.HttpContext.User;
        }

        public string GenerateToken(Domain.Models.User user, string tokenKey)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                    new Claim(_userId, user.Id.ToString()),
                    new Claim(_userType, user.UserType.ToString()),
                    new Claim(ClaimTypes.Role, user.UserType.ToString()),
                    new Claim(_userName, user.Name)
                };
            JwtSecurityToken token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );
            return tokenHandler.WriteToken(token);
        }

        private T FetchClaim<T>(string claimKey)
        {
            if (GetUser().Identity is ClaimsIdentity claimsIdentity && claimsIdentity.Claims != null && claimsIdentity.Claims.Count() > 0)
                return (T)Convert.ChangeType(claimsIdentity.Claims.FirstOrDefault(c => c.Type == claimKey).Value, typeof(T));
            else
                return default;
        }

        public int GetUserId()
        {
            return FetchClaim<Int32>(_userId);
        }
        public UserType GetUserType()
        {
            string requestId = FetchClaim<string>(_userType);
            UserType.TryParse(requestId, out UserType outResult);
            return outResult;
        }

        public string GetUserName()
        {
            return FetchClaim<string>(_userName);
        }

        public Guid GetRequestId()
        {
            string requestId = FetchClaim<string>(_requestId);
            Guid.TryParse(requestId, out Guid outResult);
            return outResult;
        }
    }
}
