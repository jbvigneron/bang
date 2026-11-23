using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class PlayCardCommandValidator : AbstractValidator<PlayCardCommand>
    {
        public PlayCardCommandValidator()
        {
            RuleFor(c => c.CardId).NotEmpty().WithMessage("La carte est obligatoire");
        }
    }
}
