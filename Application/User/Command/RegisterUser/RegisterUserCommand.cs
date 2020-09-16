using MediatR;

namespace Application.User.Command.RegisterUser
{
    public class RegisterUserCommand : IRequest<int>
    {
        public Domain.Models.User User { get; set; }
    }
}
