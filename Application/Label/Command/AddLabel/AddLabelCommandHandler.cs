using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Command.AddLabel
{
    public class AddLabelCommandHandler : IRequestHandler<AddLabelCommand, int>
    {
        private readonly IInstanceDB _instanceDB;
        public AddLabelCommandHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }

        public async Task<int> Handle(AddLabelCommand request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.AddLabel(request.Label);
        }
    }
}
