using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Command.AssignLabelToList
{
    public class AssignItemLabelCommandHandler : IRequestHandler<AssignLabelToListCommand, int>
    {
        private readonly IInstanceDB _instanceDB;

        public AssignItemLabelCommandHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(AssignLabelToListCommand request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.AssignLabelToList(request.LabelId, request.ListId);
        }
    }
}
