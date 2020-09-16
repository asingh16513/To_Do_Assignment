using MediatR;

namespace Application.ToDoLists.Command.UpdateCommand
{
    public class UpdateToDoListCommand : IRequest<int>
    {
        public Domain.Models.BaseToDoList ToDoList { get; set; }
    }
}
