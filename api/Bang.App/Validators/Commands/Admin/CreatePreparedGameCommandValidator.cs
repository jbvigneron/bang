using Bang.App.Commands.Admin;
using FluentValidation;

namespace Bang.App.Validators.Commands.Admin
{
    public sealed class CreatePreparedGameCommandValidator : AbstractValidator<CreatePreparedGameCommand>
    {
        public CreatePreparedGameCommandValidator()
        {
            RuleFor(c => c.Players).NotEmpty().WithMessage("La liste des joueurs est obligatoire");

            RuleForEach(c => c.Players).ChildRules(player =>
            {
                player.RuleFor(p => p.Name).NotEmpty().WithMessage("Le nom du joueur est obligatoire");
                player.RuleFor(p => p.RoleId).IsInEnum();
                player.RuleFor(p => p.CharacterId).IsInEnum();
            });
        }
    }
}
