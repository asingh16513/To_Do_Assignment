using Application.Helper;
using Application.Interface;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Label.Queries.GetLabels
{
    public class GetLabelListHandler : IRequestHandler<EmptyQuery<List<Domain.Models.Label>>, List<Domain.Models.Label>>
    {
        private readonly IInstanceDB _instanceDB;

        public GetLabelListHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<List<Domain.Models.Label>> Handle(EmptyQuery<List<Domain.Models.Label>> request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<ILabelDBManager>();
            return await db.GetLabels();
        }
    }
}
