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
            var playerDeck = await this.dbContext.PlayersDecks
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var player = playerDeck.Player;

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .SingleAsync(d => d.GameId == player.GameId, cancellationToken);

            var moveToGameDeck = playerDeck.Cards.Single(c => c.Id == notification.OldCard.Id);
            var moveToPlayerHand = gameDeck.Cards.First(c => c.Name == notification.NewCardName);

            playerDeck.Cards.Remove(moveToGameDeck);
            gameDeck.Cards.Add(moveToGameDeck);

            gameDeck.Cards.Remove(moveToPlayerHand);
            playerDeck.Cards.Add(moveToPlayerHand);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.playerHub
                .Clients.Group(player.Id.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, playerDeck.Cards, cancellationToken);
        }
    }
}
