using Bang.App.Commands.Game;
using FluentValidation;
using System.Linq;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(c => c.PlayerNames)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("La liste des joueurs est obligatoire")
                .Must(names => names.Count() is >= 4 and <= 7).WithMessage("Le nombre de joueurs doit être compris entre 4 et 7")
                .Must(names => names.Distinct().Count() == names.Count()).WithMessage("Les joueurs doivent avoir des noms différents");

            RuleForEach(c => c.PlayerNames)
                .NotEmpty().WithMessage("Le nom du joueur est obligatoire");
        }
    }
}
