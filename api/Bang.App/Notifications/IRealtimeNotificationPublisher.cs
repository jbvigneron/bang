using Bang.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Notifications
{
    public interface IRealtimeNotificationPublisher
    {
        Task PublishGameCreatedAsync(CurrentGame game, CancellationToken cancellationToken);

        Task PublishPlayerJoinedAsync(Guid gameId, Player player, CancellationToken cancellationToken);

        Task PublishAllPlayersJoinedAsync(Guid gameId, CurrentGame game, CancellationToken cancellationToken);

        Task PublishPlayerTurnAsync(Guid gameId, string playerName, CancellationToken cancellationToken);

        Task PublishYourTurnAsync(Guid playerId, CancellationToken cancellationToken);

        Task PublishCardsDrawnAsync(
            Guid gameId,
            int deckCount,
            string playerName,
            int cardsInHand,
            CancellationToken cancellationToken);

        Task PublishYourHandAsync(Guid playerId, IEnumerable<Card> cards, CancellationToken cancellationToken);

        Task PublishCardPlacedAsync(
            Guid gameId,
            Guid playerId,
            Guid targetPlayerId,
            Card card,
            CancellationToken cancellationToken);

        Task PublishCardDiscardedAsync(
            Guid gameId,
            Guid playerId,
            Card card,
            CancellationToken cancellationToken);

        Task PublishWeaponChangedAsync(
            Guid gameId,
            Guid playerId,
            Weapon weapon,
            CancellationToken cancellationToken);
    }
}
