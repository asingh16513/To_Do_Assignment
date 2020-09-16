using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoLists.Query.DeleteToDoListQuery
{
    public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoListQuery, int>
    {
        private readonly IInstanceDB _instanceDB;
        public DeleteToDoItemHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(DeleteToDoListQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoListDbManager>();
            return await db.DeleteToDoList(request.ItemId);
        }
    }
}
