using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class EndTurnCommandValidator : AbstractValidator<EndTurnCommand>
    {
        public EndTurnCommandValidator()
        {
            RuleFor(c => c.PlayerId).NotEmpty().WithMessage("Le joueur est obligatoire");
            RuleFor(c => c.GameId).NotEmpty().WithMessage("La partie est obligatoire");
            RuleFor(c => c.CardIds).NotNull().WithMessage("La liste des cartes à défausser est obligatoire");
        }
    }
}
