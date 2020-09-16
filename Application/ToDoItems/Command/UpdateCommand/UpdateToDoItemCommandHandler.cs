using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Command.UpdateCommand
{
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, int>
    {
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        private readonly IDTO _dtoMapper;
        public UpdateToDoItemCommandHandler(IUserManager userAccessor, IDTO dtoMapper, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _dtoMapper = dtoMapper;
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            int userId = _userAccessor.GetUserId();
            request.ToDoItem.UserId = userId;
            var db = _instanceDB.Get<IToDoItemDbManager>();
            ToDoItem item = _dtoMapper.MapItemDTOToUpdateEntity(request.ToDoItem);
            return await db.UpdateToDoItem(item);
        }
    }
}
