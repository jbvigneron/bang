using Bang.Core.Constants;
using Bang.Core.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.Core.Events.Handlers
{
    public class CardDiscardedNotificationHandler : INotificationHandler<CardDiscarded>
    {
        private readonly IHubContext<GameHub> gameHub;

        public CardDiscardedNotificationHandler(IHubContext<GameHub> gameHub)
        {
            this.gameHub = gameHub;
        }

        public Task Handle(CardDiscarded notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var card = notification.Card;

            return this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.CardDiscarded, gameId, playerId, card, cancellationToken);
        }
    }
}