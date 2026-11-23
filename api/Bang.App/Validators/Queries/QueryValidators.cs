using Bang.App.Queries;
using FluentValidation;

namespace Bang.App.Validators.Queries
{
    public sealed class GameQueryValidator : AbstractValidator<GameQuery>
    {
        public GameQueryValidator()
        {
            RuleFor(q => q.GameId).NotEmpty().WithMessage("L'identifiant de la partie est obligatoire");
        }
    }

    public sealed class CardQueryValidator : AbstractValidator<CardQuery>
    {
        public CardQueryValidator()
        {
            RuleFor(q => q.CardId).NotEmpty().WithMessage("L'identifiant de la carte est obligatoire");
        }
    }

    public sealed class PlayerQueryValidator : AbstractValidator<PlayerQuery>
    {
        public PlayerQueryValidator()
        {
            RuleFor(q => q.PlayerId).NotEmpty().WithMessage("L'identifiant du joueur est obligatoire");
        }
    }

    public sealed class PlayerHandQueryValidator : AbstractValidator<PlayerHandQuery>
    {
        public PlayerHandQueryValidator()
        {
            RuleFor(q => q.PlayerId).NotEmpty().WithMessage("L'identifiant du joueur est obligatoire");
        }
    }

    public sealed class PlayerIdQueryValidator : AbstractValidator<PlayerIdQuery>
    {
        public PlayerIdQueryValidator()
        {
            RuleFor(q => q.GameId).NotEmpty().WithMessage("L'identifiant de la partie est obligatoire");
            RuleFor(q => q.PlayerName).NotEmpty().WithMessage("Le nom du joueur est obligatoire");
        }
    }
}
