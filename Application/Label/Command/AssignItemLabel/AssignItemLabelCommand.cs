using MediatR;

namespace Application.Label.Command.AssignItemLabel
{
    public class AssignItemLabelCommand : IRequest<int>
    {
        public int LabelId { get; set; }
        public int[] ItemId { get; set; }
    }
}
