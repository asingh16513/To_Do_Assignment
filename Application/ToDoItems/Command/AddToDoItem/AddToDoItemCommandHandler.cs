using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Command.AddToDoItem
{
    public class AddToDoItemCommandHandler : IRequestHandler<AddToDoItemCommand, int>
    {
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        private readonly IDTO _dTO;
        public AddToDoItemCommandHandler(IUserManager userAccessor, IDTO dTO, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _dTO = dTO;
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
        {
            int userId = _userAccessor.GetUserId();
            request.ToDoItem.UserId = userId;
            ToDoItem item = _dTO.MapItemDTOToAddEntity(request.ToDoItem);
            var db = _instanceDB.Get<IToDoItemDbManager>();
            return await db.AddToDoItem(item);
        }
    }
}
