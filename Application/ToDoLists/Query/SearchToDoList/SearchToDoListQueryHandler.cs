using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoLists.Query.SearchToDoList
{
    public class SearchToDoListQueryHandler : IRequestHandler<SearchToDoListQuery, List<ToDoListExt>>
    {
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        public SearchToDoListQueryHandler(IUserManager userAccessor, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _instanceDB = instanceDB;
        }
        public async Task<List<ToDoListExt>> Handle(SearchToDoListQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoListDbManager>();
            return await db.SearchToDoList(_userAccessor.GetUserId(), request.SearchFilter.SearchString, request.SearchFilter.PageNumber,
                request.SearchFilter.PageSize);

        }
    }
}
