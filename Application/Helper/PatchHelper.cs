using Application.Interface;
using Application.ToDoItems.Command.PatchUpdateToDoItem;
using Application.ToDoItems.Command.UpdateCommand;
using Application.ToDoLists.Command.UpdateCommand;
using Application.ToDoLists.PatchUpdateToDoList;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace Application.Helper
{
    /// <summary>
    /// Class holds path methods to convert from Jsonpatchdocument to Command
    /// </summary>
    public class PatchHelper : IPatchToDo
    {
        public UpdatePatchToDoItemCommand CommandToPatch(int id, JsonPatchDocument<UpdateToDoItemCommand> jsonPatchDocument)
        {
            UpdatePatchToDoItemCommand command = new UpdatePatchToDoItemCommand
            {
                ItemId = id,
                JsonPatchDocument = jsonPatchDocument
            };
            return command;
        }

        public UpdateToDoItemCommand ItemToCommand(ToDoItem item)
        {
            UpdateToDoItemCommand command = new UpdateToDoItemCommand
            {
                ToDoItem = item
            };
            return command;
        }

        public UpdatePatchToDoListCommand CommandToPatch(int id, JsonPatchDocument<UpdateToDoListCommand> jsonPatchDocument)
        {
            UpdatePatchToDoListCommand command = new UpdatePatchToDoListCommand
            {
                ItemId = id,
                JsonPatchDocument = jsonPatchDocument
            };
            return command;
        }

        public UpdateToDoListCommand ListToCommand(BaseToDoList item)
        {
            UpdateToDoListCommand command = new UpdateToDoListCommand
            {
                ToDoList = item
            };
            return command;
        }

        public UpdateToDoListCommand ListToCommand(ToDoList item)
        {
            throw new NotImplementedException();
        }
    }
}