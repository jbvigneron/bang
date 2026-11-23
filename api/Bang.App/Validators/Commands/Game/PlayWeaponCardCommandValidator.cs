using Bang.App.Commands.Game;
using FluentValidation;

namespace Bang.App.Validators.Commands.Game
{
    public sealed class PlayWeaponCardCommandValidator : AbstractValidator<PlayWeaponCardCommand>
    {
        public PlayWeaponCardCommandValidator()
        {
            RuleFor(c => c.Card).NotNull().WithMessage("La carte est obligatoire");
        }
    }
}
