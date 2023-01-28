﻿using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.EventsHandlers
{
    public class PlayerDeckPrepareHandler : INotificationHandler<PlayerDeckPrepare>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;

        public PlayerDeckPrepareHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
        }

        public async Task Handle(PlayerDeckPrepare notification, CancellationToken cancellationToken)
        {
            var playerDeck = new PlayerDeck
            {
                PlayerId = notification.PlayerId,
                Cards = new List<Card>()
            };

            var player = await this.dbContext.Players.FirstAsync(p => p.Id == notification.PlayerId, cancellationToken);

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .FirstAsync(d => d.Game.Players.Any(p => p.Id == notification.PlayerId), cancellationToken);

            var game = gameDeck.Game;

            for (int i = 1; i <= player.Lives; i++)
            {
                var card = gameDeck.Cards.First();

                playerDeck.Cards.Add(card);
                player.DeckCount++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            await this.dbContext.PlayersDecks.AddAsync(playerDeck, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.DeckUpdated, game.Id, game.DeckCount, cancellationToken);
        }
    }
}