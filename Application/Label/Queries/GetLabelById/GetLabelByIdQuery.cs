using MediatR;

namespace Application.Label.Queries.GetLabelById
{
    public class GetLabelByIdQuery : IRequest<Domain.Models.Label>
    {
        public int LabelId { get; set; }
    }
}
