using MediatR;

namespace Application.Label.Command.AssignLabelToList
{
    public class AssignLabelToListCommand : IRequest<int>
    {
        public int LabelId { get; set; }
        public int[] ListId { get; set; }
    }
}
