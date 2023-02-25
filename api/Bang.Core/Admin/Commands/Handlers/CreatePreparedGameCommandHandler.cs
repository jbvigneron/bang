using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Admin.Commands.Handlers
{
    public class CreatePreparedGameCommandHandler : IRequestHandler<CreatePreparedGameCommand, Guid>
    {
        private readonly BangDbContext dbContext;

        public CreatePreparedGameCommandHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreatePreparedGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid();
            var players = request.Players;

            var cards = await this.dbContext.Cards.OrderBy(c => Guid.NewGuid()).ToListAsync(cancellationToken);

            var game = new Game
            {
                Id = gameId,
                Status = GameStatus.WaitingForPlayers,
                Players = players.Select(player =>
                {
                    var character = dbContext.Characters.First(c => c.Id == player.CharacterId);
                    var role = dbContext.Roles.First(r => r.Id == player.RoleId);
                    var isSheriff = role.Id == RoleKind.Sheriff;

                    return new Player
                    {
                        Name = player.Name,
                        Role = role,
                        IsSheriff = isSheriff,
                        Character = character,
                        Lives = character.Lives + (isSheriff ? 1 : 0),
                        Status = PlayerStatus.NotReady,
                        Weapon = dbContext.Weapons.First(w => w.Id == WeaponKind.Colt45)
                    };
                }).ToList(),
                CurrentPlayerName = players.Single(info => info.RoleId == RoleKind.Sheriff).Name,
                DeckCount = cards.Count,
            };

            var deck = new GameDeck
            {
                GameId = gameId,
                Cards = cards
            };

            var discard = new GameDiscard
            {
                GameId = gameId
            };

            await this.dbContext.Games.AddAsync(game, cancellationToken);
            await this.dbContext.GamesDecks.AddAsync(deck, cancellationToken);
            await this.dbContext.GamesDiscardPiles.AddAsync(discard, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return gameId;
        }
    }
}
