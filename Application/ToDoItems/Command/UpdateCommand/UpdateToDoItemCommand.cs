using MediatR;

namespace Application.ToDoItems.Command.UpdateCommand
{
    public class UpdateToDoItemCommand : IRequest<int>
    {
        public Domain.Models.BaseToDoItem ToDoItem { get; set; }
    }
}
