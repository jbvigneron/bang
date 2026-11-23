using Bang.App.Notifications;
using Bang.Domain.Constants;
using Bang.Domain.Entities;
using Bang.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.WebApi.SignalR
{
    public class SignalRRealtimeNotificationPublisher : IRealtimeNotificationPublisher
    {
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;
        private readonly IHubContext<PublicHub> publicHub;

        public SignalRRealtimeNotificationPublisher(
            IHubContext<GameHub> gameHub,
            IHubContext<PlayerHub> playerHub,
            IHubContext<PublicHub> publicHub)
        {
            this.gameHub = gameHub;
            this.playerHub = playerHub;
            this.publicHub = publicHub;
        }

        public Task PublishGameCreatedAsync(CurrentGame game, CancellationToken cancellationToken) =>
            this.publicHub.Clients.All.SendAsync(Events.Public.GameCreated, game, cancellationToken);

        public Task PublishPlayerJoinedAsync(Guid gameId, Player player, CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.PlayerJoined, gameId, player, cancellationToken);

        public Task PublishAllPlayersJoinedAsync(Guid gameId, CurrentGame game, CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.AllPlayerJoined, gameId, game, cancellationToken);

        public Task PublishPlayerTurnAsync(Guid gameId, string playerName, CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.PlayerTurn, gameId, playerName, cancellationToken);

        public Task PublishYourTurnAsync(Guid playerId, CancellationToken cancellationToken) =>
            this.playerHub.Clients.Group(playerId.ToString())
                .SendAsync(Events.Player.YourTurn, cancellationToken);

        public Task PublishCardsDrawnAsync(
            Guid gameId,
            int deckCount,
            string playerName,
            int cardsInHand,
            CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.CardsDrawn, gameId, deckCount, playerName, cardsInHand, cancellationToken);

        public Task PublishYourHandAsync(
            Guid playerId,
            IEnumerable<Card> cards,
            CancellationToken cancellationToken) =>
            this.playerHub.Clients.Group(playerId.ToString())
                .SendAsync(Events.Player.YourHand, cards, cancellationToken);

        public Task PublishCardPlacedAsync(
            Guid gameId,
            Guid playerId,
            Guid targetPlayerId,
            Card card,
            CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.CardDiscarded, gameId, playerId, targetPlayerId, card, cancellationToken);

        public Task PublishCardDiscardedAsync(
            Guid gameId,
            Guid playerId,
            Card card,
            CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.CardDiscarded, gameId, playerId, card, cancellationToken);

        public Task PublishWeaponChangedAsync(
            Guid gameId,
            Guid playerId,
            Weapon weapon,
            CancellationToken cancellationToken) =>
            this.gameHub.Clients.Group(gameId.ToString())
                .SendAsync(Events.Game.WeaponChanged, gameId, playerId, weapon, cancellationToken);
    }
}
