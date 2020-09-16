using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Command.AssignItemLabel
{
    public class AssignItemLabelCommandHandler : IRequestHandler<AssignItemLabelCommand, int>
    {
        private readonly IInstanceDB _instanceDB;

        public AssignItemLabelCommandHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(AssignItemLabelCommand request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.AssignLabelToItem(request.LabelId, request.ItemId);
        }
    }
}
