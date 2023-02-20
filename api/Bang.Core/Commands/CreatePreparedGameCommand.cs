using Bang.Models.Enums;
using MediatR;

namespace Bang.Core.Commands
{
    public class CreatePreparedGameCommand : IRequest<Guid>
    {
        public CreatePreparedGameCommand(IEnumerable<(string Name, CharacterKind Character, RoleKind Role)> players)
        {
            this.Players = players;
        }

        public IEnumerable<(string Name, CharacterKind Character, RoleKind Role)> Players { get; set; }
    }
}
