using Application.Interface;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Query.DeleteToDoItemQuery
{
    public class DeleteToDoItemQueryHandler : IRequestHandler<DeleteToDoItemQuery, int>
    {
        private readonly IInstanceDB _instanceDB;
        public DeleteToDoItemQueryHandler(IInstanceDB instanceDB)
        {
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(DeleteToDoItemQuery request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoItemDbManager>();
            return await db.DeleteToDoItem(request.ItemId);
        }
    }
}
