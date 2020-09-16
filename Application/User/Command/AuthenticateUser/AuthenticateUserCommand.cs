using Domain.Models;
using MediatR;

namespace Application.User.Command.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<UserAuthResult>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
