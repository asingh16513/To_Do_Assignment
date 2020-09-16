using MediatR;

namespace Application.Label.Queries.DeleteLabelById
{
    public class DeleteLabelByIdQuery : IRequest<int>
    {
        public int LableId { get; set; }
    }
}
