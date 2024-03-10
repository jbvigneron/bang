using Bang.App.Constants;
using Bang.App.Hubs;
using Bang.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardsDrawnHandler : INotificationHandler<CardsDrawn>
    {
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public CardsDrawnHandler(IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(CardsDrawn notification, CancellationToken cancellationToken)
        {
            var player = notification.Player;
            var playerId = player.Id;
            var playerName = player.Name;

            var game = notification.Game;
            var gameId = game.Id;

            var cards = notification.Cards;

            await this.gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.CardsDrawn, game.Id, game.DeckCount, playerName, player.CardsInHand, cancellationToken);

            await this.playerHub
                .Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.YourHand, cards, cancellationToken);
        }
    }
}