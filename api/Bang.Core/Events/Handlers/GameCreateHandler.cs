using Bang.Core.Constants;
using Bang.Core.Exceptions;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class GameCreateHandler : INotificationHandler<GameCreate>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PublicHub> publicHub;

        private readonly List<RoleKind> roles = new()
        {
            RoleKind.Sheriff,
            RoleKind.Renegade,
            RoleKind.Outlaw,
            RoleKind.Outlaw
        };

        public GameCreateHandler(BangDbContext dbContext, IHubContext<PublicHub> publicHub)
        {
            this.dbContext = dbContext;
            this.publicHub = publicHub;
        }

        public async Task Handle(GameCreate notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerNames = notification.PlayerNames;

            if (playerNames.Distinct().Count() != playerNames.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents", gameId);
            }

            var cards = await this.dbContext.Cards.OrderBy(c => Guid.NewGuid()).ToListAsync(cancellationToken);

            var game = new Game
            {
                Id = gameId,
                Status = GameStatus.WaitingForPlayers,
                Players = new List<Player>(),
                DeckCount = cards.Count
            };

            this.DetermineAvailablesRoles(playerNames.Count());

            foreach (var playerName in playerNames)
            {
                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatus.NotReady,
                    Role = await this.GetRandomRoleAsync(cancellationToken),
                };

                if (player.Role.Id == RoleKind.Sheriff)
                {
                    player.IsSheriff = true;
                    game.CurrentPlayerName = player.Name;
                }

                game.Players.Add(player);
            }

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

            await this.publicHub.Clients.All.SendAsync(HubMessages.Public.GameCreated, game, cancellationToken);
        }

        private void DetermineAvailablesRoles(int numberOfPlayers)
        {
            if (numberOfPlayers < 4 || numberOfPlayers > 7)
                throw new ArgumentOutOfRangeException(nameof(numberOfPlayers), "Le nombre de joueurs doit être compris entre 4 et 7");

            if (numberOfPlayers >= 5)
                this.roles.Add(RoleKind.DeputySheriff);

            if (numberOfPlayers >= 6)
                this.roles.Add(RoleKind.Outlaw);

            if (numberOfPlayers == 7)
                this.roles.Add(RoleKind.DeputySheriff);
        }

        private Task<Role> GetRandomRoleAsync(CancellationToken cancellationToken)
        {
            var index = new Random().Next(this.roles.Count);
            var roleId = this.roles[index];

            this.roles.RemoveAt(index);

            return this.dbContext.Roles.SingleAsync(r => r.Id == roleId, cancellationToken);
        }
    }
}
