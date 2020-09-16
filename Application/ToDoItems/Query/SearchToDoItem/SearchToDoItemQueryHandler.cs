using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Query.SearchToDoItem
{
    public class SearchToDoItemQueryHandler : IRequestHandler<SearchToDoItemQuery, List<ToDoItemExt>>
    {
        private readonly IInstanceDB _instanceDB;
        private readonly IUserManager _userAccessor;
        public SearchToDoItemQueryHandler(IUserManager userAccessor, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _instanceDB = instanceDB;
        }
        public async Task<List<ToDoItemExt>> Handle(SearchToDoItemQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoItemDbManager>();
            return await db.SearchToDoItems(_userAccessor.GetUserId(), request.SearchFilter.SearchString, request.SearchFilter.PageNumber,
                request.SearchFilter.PageSize);
        }
    }
}
