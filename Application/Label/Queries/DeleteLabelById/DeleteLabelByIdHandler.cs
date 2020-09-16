using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Queries.DeleteLabelById
{
    public class DeleteLabelByIdHandler : IRequestHandler<DeleteLabelByIdQuery, int>
    {
        private readonly IInstanceDB _instanceDB;

        public DeleteLabelByIdHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(DeleteLabelByIdQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.DeleteLabelById(request.LableId);
        }
    }
}
