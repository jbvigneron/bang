using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.NotificationsHandlers
{
    public class SwitchCardHandler : INotificationHandler<SwitchCard>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PlayerHub> playerHub;

        public SwitchCardHandler(BangDbContext dbContext, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;
            this.playerHub = playerHub;
        }

        public async Task Handle(SwitchCard notification, CancellationToken cancellationToken)
        {
            var hand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var player = hand.Player;

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .SingleAsync(d => d.GameId == player.GameId, cancellationToken);

            var moveToGameDeck = hand.Cards.Single(c => c.Id == notification.OldCard.Id);
            var moveToPlayerHand = gameDeck.Cards.First(c => c.Name == notification.NewCardName);

            hand.Cards.Remove(moveToGameDeck);
            gameDeck.Cards.Add(moveToGameDeck);

            gameDeck.Cards.Remove(moveToPlayerHand);
            hand.Cards.Add(moveToPlayerHand);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.playerHub
                .Clients.Group(player.Id.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
