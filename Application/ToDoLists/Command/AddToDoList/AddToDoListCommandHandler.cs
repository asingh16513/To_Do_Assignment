using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoLists.Command.AddToDoList
{
    public class AddToDoListCommandHandler : IRequestHandler<AddToDoListCommand, int>
    {
        private readonly IUserManager _userAccessor;
        private readonly IInstanceDB _instanceDB;
        private readonly IDTO _dTO;
        public AddToDoListCommandHandler(IUserManager userAccessor, IDTO dTO, IInstanceDB instanceDB)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _dTO = dTO;
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(AddToDoListCommand request, CancellationToken cancellationToken)
        {
            int userId = _userAccessor.GetUserId();
            request.ToDoList.UserId = userId;
            var db = _instanceDB.Get<IToDoListDbManager>();
            ToDoList list = _dTO.MapListDTOToAddEntity(request.ToDoList);
            return await db.AddToDoList(list);
        }


    }
}
