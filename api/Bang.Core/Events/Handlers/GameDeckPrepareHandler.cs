using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class GameDeckPrepareHandler : INotificationHandler<GameDeckPrepare>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;

        public GameDeckPrepareHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
        }

        public async Task Handle(GameDeckPrepare notification, CancellationToken cancellationToken)
        {
            var cards = await dbContext.Cards.OrderBy(c => Guid.NewGuid()).ToListAsync(cancellationToken);

            var deck = new GameDeck
            {
                GameId = notification.GameId,
                Cards = cards
            };

            await dbContext.GamesDecks.AddAsync(deck, cancellationToken);

            var game = await dbContext.Games.SingleAsync(g => g.Id == notification.GameId, cancellationToken);
            game.DeckCount = cards.Count;

            await dbContext.SaveChangesAsync(cancellationToken);

            await gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.GameDeckReady, game.Id, cards.Count, cancellationToken);
        }
    }
}
