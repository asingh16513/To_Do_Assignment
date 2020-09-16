using Application.ToDoLists.Command.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.ToDoLists.PatchUpdateToDoList
{
    public class UpdatePatchToDoListCommand : IRequest<int>
    {
        public JsonPatchDocument<UpdateToDoListCommand> JsonPatchDocument { get; set; }
        public int ItemId { get; set; }
    }
}
