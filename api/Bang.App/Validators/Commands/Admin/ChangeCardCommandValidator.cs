using Bang.App.Commands.Admin;
using FluentValidation;

namespace Bang.App.Validators.Commands.Admin
{
    public sealed class ChangeCardCommandValidator : AbstractValidator<ChangeCardCommand>
    {
        public ChangeCardCommandValidator()
        {
            RuleFor(c => c.PlayerId).NotEmpty().WithMessage("Le joueur est obligatoire");
            RuleFor(c => c.OldCardId).NotEmpty().WithMessage("La carte à remplacer est obligatoire");
            RuleFor(c => c.NewCardName).NotEmpty().WithMessage("Le nom de la nouvelle carte est obligatoire");
        }
    }
}
