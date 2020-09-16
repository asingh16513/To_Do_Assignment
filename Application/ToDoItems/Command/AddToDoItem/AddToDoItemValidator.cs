using Domain.Models;
using FluentValidation;

namespace Application.ToDoItems.Command.AddToDoItem
{
    public class AddToDoItemValidator : AbstractValidator<BaseToDoItem>
    {
        public AddToDoItemValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.IsComplete).NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
