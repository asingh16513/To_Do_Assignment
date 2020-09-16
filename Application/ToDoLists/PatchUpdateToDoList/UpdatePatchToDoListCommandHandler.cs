using Application.Interface;
using Domain.Models;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ToDoLists.PatchUpdateToDoList
{
    public class UpdatePatchToDoListCommandHandler : IRequestHandler<UpdatePatchToDoListCommand, int>
    {
        private readonly IDTO _dtoMapper;
        private readonly IPatchToDo _patchToDo;
        private readonly IInstanceDB _instanceDB;

        public UpdatePatchToDoListCommandHandler(IPatchToDo patchToDo, IDTO dtoMapper, IInstanceDB instanceDB)
        {
            _patchToDo = patchToDo;
            _dtoMapper = dtoMapper;
            _instanceDB = instanceDB;
        }
        public async Task<int> Handle(UpdatePatchToDoListCommand request, CancellationToken cancellationToken)
        {
            var db = _instanceDB.Get<IToDoListDbManager>();
            ToDoList item = _dtoMapper.MapListDTOToUpdateEntity(await db.GetToDoListById(request.ItemId));
            var patch = _patchToDo.ListToCommand(item);
            if (item != null)
            {
                request.JsonPatchDocument.ApplyTo(patch);
            }
            return 1;
        }
    }
}
