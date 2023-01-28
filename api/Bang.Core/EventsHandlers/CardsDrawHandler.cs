using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.EventsHandlers
{
    public class CardsDrawHandler : INotificationHandler<CardsDraw>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public CardsDrawHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(CardsDraw notification, CancellationToken cancellationToken)
        {
            var playerDeck = await this.dbContext.PlayersDecks
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var player = playerDeck.Player;

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .SingleAsync(d => d.GameId == player.GameId, cancellationToken);

            var game = gameDeck.Game;

            for (var i = 1; i <= 2; i++)
            {
                var card = gameDeck.Cards.First();

                playerDeck.Cards.Add(card);
                player.DeckCount++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            player.HasDrawnCards = true;

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.CardsDrawn, game.Id, game.DeckCount, player.Name, player.DeckCount, cancellationToken);

            await this.playerHub
                .Clients.Group(player.Id.ToString())
                .SendAsync(HubMessages.Player.NewCards, playerDeck.Cards, cancellationToken);
        }
    }
}
