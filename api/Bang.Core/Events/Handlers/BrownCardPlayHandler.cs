using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class BrownCardPlayHandler : INotificationHandler<BrownCardPlay>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public BrownCardPlayHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;

            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(BrownCardPlay notification, CancellationToken cancellationToken)
        {
            var playerId = notification.PlayerId;
            var gameId = notification.GameId;
            var cardId = notification.CardId;

            var hand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .SingleAsync(p => p.PlayerId == playerId, cancellationToken);

            var discardPile = await this.dbContext.GamesDiscardPiles
                .Include(d => d.Cards)
                .SingleAsync(g => g.GameId == gameId, cancellationToken);

            var card = hand.Cards!.First(c => c.Id == cardId);

            hand.Cards!.Remove(card);
            discardPile.Cards!.Add(card);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.CardPlaced, gameId, playerId, card, cancellationToken);

            await this.playerHub
                .Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
