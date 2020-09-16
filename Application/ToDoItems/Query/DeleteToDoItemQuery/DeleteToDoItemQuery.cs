using MediatR;

namespace Application.ToDoItems.Query.DeleteToDoItemQuery
{
    public class DeleteToDoItemQuery : IRequest<int>
    {
        public int ItemId { get; set; }
    }
}
