using Domain.Models;
using MediatR;

namespace Application.ToDoItems.Command.AddToDoItem
{
    public class AddToDoItemCommand : IRequest<int>
    {
        public BaseToDoItem ToDoItem { get; set; }
    }
}
