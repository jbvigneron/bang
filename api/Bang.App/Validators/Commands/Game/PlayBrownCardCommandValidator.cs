using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class PlayBrownCardCommandValidator : AbstractValidator<PlayBrownCardCommand>
    {
        public PlayBrownCardCommandValidator()
        {
            RuleFor(c => c.Card).NotNull().WithMessage("La carte est obligatoire");
        }
    }
}
