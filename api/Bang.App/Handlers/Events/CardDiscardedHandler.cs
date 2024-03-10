using Bang.App.Constants;
using Bang.App.Hubs;
using Bang.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardDiscardedHandler : INotificationHandler<CardDiscarded>
    {
        private readonly IHubContext<GameHub> gameHub;

        public CardDiscardedHandler(IHubContext<GameHub> gameHub)
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