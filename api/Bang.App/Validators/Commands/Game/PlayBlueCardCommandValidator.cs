using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class PlayBlueCardCommandValidator : AbstractValidator<PlayBlueCardCommand>
    {
        public PlayBlueCardCommandValidator()
        {
            RuleFor(c => c.Card).NotNull().WithMessage("La carte est obligatoire");

            When(c => c.Card is not null && c.Card.RequireOpponent, () =>
            {
                RuleFor(c => c.OpponentId)
                    .NotNull().WithMessage("Veuillez cibler un adversaire pour cette carte.");
            });
        }
    }
}
