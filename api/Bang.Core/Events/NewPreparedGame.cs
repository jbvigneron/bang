using Bang.Models.Enums;
using MediatR;

namespace Bang.Core.Events
{
    public class NewPreparedGame : INotification
    {
        public NewPreparedGame(Guid gameId, IEnumerable<(string Name, CharacterKind Character, RoleKind Role)> players)
        {
            this.GameId = gameId;
            this.Players = players;
        }

        public Guid GameId { get; }
        public IEnumerable<(string Name, CharacterKind Character, RoleKind Role)> Players { get; set; }
    }
}
