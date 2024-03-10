using Bang.App.Constants;
using Bang.Domain.Entities;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bang.Tests.Drivers.Hubs
{
    public class GameHubDriver
    {
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        private HubConnection connection;
        private readonly IList<string> messages = new List<string>();

        public GameHubDriver(GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = this.httpClientFactoryContext.Factory.Server;

            this.connection = HubHelper.ConnectToOpenHub(server, "http://localhost/GameHub");

            this.connection.On<Guid, Player>(HubMessages.Game.PlayerJoined, (gameId, player) =>
            {
                this.messages.Add(HubMessages.Game.PlayerJoined);
                var players = this.gameContext.Current.Players;

                for (int i = 0; i < players.Count; i++)
                    if (players[i].Id == player.Id)
                        players[i] = player;
            });

            this.connection.On<Guid, CurrentGame>(HubMessages.Game.AllPlayerJoined, (gameId, game) =>
            {
                this.messages.Add(HubMessages.Game.AllPlayerJoined);
                this.gameContext.Current = game;
            });

            this.connection.On<Guid, string>(HubMessages.Game.PlayerTurn, (gameId, name) =>
            {
                this.messages.Add(HubMessages.Game.PlayerTurn);
                this.gameContext.Current.CurrentPlayerName = name;
            });

            this.connection.On<Guid, int, string, int>(HubMessages.Game.CardsDrawn, (gameId, gameDeckCount, playerName, cardsInHand) =>
            {
                this.messages.Add(HubMessages.Game.CardsDrawn);
                this.gameContext.Current.DeckCount = gameDeckCount;
                this.gameContext.Current.Players.Single(p => p.Name == playerName).CardsInHand = cardsInHand;
            });

            await this.connection.StartAsync();
        }

        public Task SubscribeToMessagesAsync() =>
            this.connection.InvokeAsync("Subscribe", this.gameContext.Current.Id);

        public void CheckMessage(string message) =>
            HubHelper.CheckMessages(this.messages, message);
    }
}