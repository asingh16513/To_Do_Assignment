using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Command.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IInstanceDB _instanceDB;
        private readonly IMD5Hash _hashHelper;
        public RegisterUserCommandHandler(IMD5Hash hashHelper, IInstanceDB instanceDB)
        {
            _hashHelper = hashHelper;
            _instanceDB = instanceDB;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IUserDbManager>();
            var base64EncodedPwd = System.Convert.FromBase64String(request.User.Password);
            var passWord = System.Text.Encoding.UTF8.GetString(base64EncodedPwd);
            request.User.Password = _hashHelper.GetMD5Hash(passWord);
            var result = await db.RegisterUser(request.User);
            return result;
        }
    }
}
