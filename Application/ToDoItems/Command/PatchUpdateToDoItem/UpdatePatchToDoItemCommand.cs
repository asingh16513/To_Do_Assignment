using Application.ToDoItems.Command.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.ToDoItems.Command.PatchUpdateToDoItem
{
    public class UpdatePatchToDoItemCommand : IRequest<int>
    {
        public JsonPatchDocument<UpdateToDoItemCommand> JsonPatchDocument { get; set; }
        public int ItemId { get; set; }
    }
}
