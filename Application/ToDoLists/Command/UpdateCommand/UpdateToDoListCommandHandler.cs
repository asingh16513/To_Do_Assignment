using Application.Interface;
using Application.ToDoLists.Command.UpdateCommand;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoItems.Command.UpdateCommand
{
    public class UpdateToDoListCommandHandler : IRequestHandler<UpdateToDoListCommand, int>
    {
        private readonly IDTO _dtoMapper;
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        public UpdateToDoListCommandHandler(IUserManager userAccessor, IDTO dtoMapper, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _dtoMapper = dtoMapper;
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
        {
            int userId = _userAccessor.GetUserId();
            request.ToDoList.UserId = userId;
            ToDoList list = _dtoMapper.MapListDTOToUpdateEntity(request.ToDoList);
            var db = _instanceDB.Get<IToDoListDbManager>();
            return await db.UpdateToDoList(list);
        }
    }
}
