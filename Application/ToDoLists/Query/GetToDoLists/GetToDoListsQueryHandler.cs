using Application.Helper;
using Application.Interface;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Query.GetToDoLists
{
    public class GetToDoListsQueryHandler : IRequestHandler<EmptyQuery<List<Domain.Models.ToDoListExt>>, List<Domain.Models.ToDoListExt>>
    {
        private readonly IInstanceDB _instanceDB;
        private readonly IUserManager _userAccessor;
        public GetToDoListsQueryHandler(IUserManager userAccessor, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _instanceDB = instanceDB;
        }
        public async Task<List<Domain.Models.ToDoListExt>> Handle(EmptyQuery<List<Domain.Models.ToDoListExt>> request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoListDbManager>();
            return await db.GetToDoList(_userAccessor.GetUserId());
        }
    }
}
