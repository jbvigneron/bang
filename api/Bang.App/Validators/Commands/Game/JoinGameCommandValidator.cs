using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class JoinGameCommandValidator : AbstractValidator<JoinGameCommand>
    {
        public JoinGameCommandValidator()
        {
            RuleFor(c => c.GameId).NotEmpty().WithMessage("L'identifiant de la partie est obligatoire");
            RuleFor(c => c.PlayerName).NotEmpty().WithMessage("Le nom du joueur est obligatoire");
        }
    }
}
