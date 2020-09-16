using MediatR;

namespace Application.Label.Command.AddLabel
{
    public class AddLabelCommand : IRequest<int>
    {
        public Domain.Models.Label Label { get; set; }
    }
}
