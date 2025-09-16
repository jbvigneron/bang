using Bang.Core.Constants;
using Bang.Core.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.Core.Events.Handlers
{
    public class CardPlacedNotificationHandler : INotificationHandler<CardPlaced>
    {
        private readonly IHubContext<GameHub> gameHub;

        public CardPlacedNotificationHandler(IHubContext<GameHub> gameHub)
        {
            this.gameHub = gameHub;
        }

        public Task Handle(CardPlaced notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var targetPlayerId = notification.TargetPlayerId;
            var card = notification.Card;

            return this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.CardDiscarded, gameId, playerId, targetPlayerId, card, cancellationToken);
        }
    }
}