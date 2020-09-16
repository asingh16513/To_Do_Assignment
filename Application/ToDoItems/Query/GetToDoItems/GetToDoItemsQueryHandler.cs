using Application.Helper;
using Application.Interface;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Query.GetToDoItems
{
    public class GetToDoItemsQueryHandler : IRequestHandler<EmptyQuery<List<Domain.Models.ToDoItemExt>>, List<Domain.Models.ToDoItemExt>>
    {
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        public GetToDoItemsQueryHandler(IUserManager userAccessor, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _instanceDB = instanceDB;
        }
        public async Task<List<Domain.Models.ToDoItemExt>> Handle(EmptyQuery<List<Domain.Models.ToDoItemExt>> request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoItemDbManager>();
            return await db.GetToDoItems(_userAccessor.GetUserId());
        }
    }
}
