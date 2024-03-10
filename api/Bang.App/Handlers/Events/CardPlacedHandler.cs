﻿using Bang.App.Constants;
using Bang.App.Hubs;
using Bang.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardPlacedHandler : INotificationHandler<CardPlaced>
    {
        private readonly IHubContext<GameHub> gameHub;

        public CardPlacedHandler(IHubContext<GameHub> gameHub)
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