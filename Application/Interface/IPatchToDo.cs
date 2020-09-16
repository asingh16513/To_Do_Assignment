using Application.ToDoItems.Command.PatchUpdateToDoItem;
using Application.ToDoItems.Command.UpdateCommand;
using Application.ToDoLists.Command.UpdateCommand;
using Application.ToDoLists.PatchUpdateToDoList;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interface
{
    public interface IPatchToDo
    {
        UpdatePatchToDoItemCommand CommandToPatch(int id, JsonPatchDocument<UpdateToDoItemCommand> jsonPatchDocument);
        UpdateToDoItemCommand ItemToCommand(ToDoItem item);

        UpdatePatchToDoListCommand CommandToPatch(int id, JsonPatchDocument<UpdateToDoListCommand> jsonPatchDocument);
        UpdateToDoListCommand ListToCommand(ToDoList item);
    }
}
