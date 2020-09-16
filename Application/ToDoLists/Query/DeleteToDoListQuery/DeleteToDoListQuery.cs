using MediatR;

namespace Application.ToDoLists.Query.DeleteToDoListQuery
{
    public class DeleteToDoListQuery : IRequest<int>
    {

        public int ItemId { get; set; }
    }
}
