using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Queries.GetLabelById
{
    public class GetLabelByIdHandler : IRequestHandler<GetLabelByIdQuery, Domain.Models.Label>
    {
        private readonly IInstanceDB _instanceDB;

        public GetLabelByIdHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<Domain.Models.Label> Handle(GetLabelByIdQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.GetLabelById(request.LabelId);
        }
    }
}
