using FluentValidation;

namespace Application.Label.Command.AddLabel
{
    public class LabelValidator : AbstractValidator<Domain.Models.Label>
    {
        public LabelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
