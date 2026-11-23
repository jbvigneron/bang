using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class DrawCardsCommandValidator : AbstractValidator<DrawCardsCommand>
    {
        public DrawCardsCommandValidator()
        {
            RuleFor(c => c.PlayerId).NotEmpty().WithMessage("Le joueur est obligatoire");
            RuleFor(c => c.GameId).NotEmpty().WithMessage("La partie est obligatoire");
        }
    }
}
