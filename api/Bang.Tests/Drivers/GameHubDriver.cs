using Bang.Core.Constants;
using Bang.Database.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers
{
    public class GameHubDriver
    {
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryContext httpClientFactoryContext;

        private readonly IList<string> messages = new List<string>();
        private HubConnection connection;

        public GameHubDriver(GameContext gameContext, HttpClientFactoryContext httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = this.httpClientFactoryContext.Factory.Server;
            var connection = SignalRHelper.ConnectToOpenHub(server, "http://localhost/GameHub");

            connection.On<Guid, int>(HubMessages.Game.GameDeckReady, (gameId, deckCount) =>
            {
                this.messages.Add(HubMessages.Game.GameDeckReady);
                this.gameContext.Current.DeckCount = deckCount;
            });

            connection.On<Guid, Player>(HubMessages.Game.PlayerJoin, (gameId, player) =>
            {
                this.messages.Add(HubMessages.Game.PlayerJoin);
                var players = gameContext.Current.Players;

                for (int i = 0; i < players.Count; i++)
                    if (players[i].Id == player.Id)
                        players[i] = player;
            });

            connection.On<Guid, int>(HubMessages.Game.DeckUpdated, (gameId, deckCount) =>
            {
                this.messages.Add(HubMessages.Game.DeckUpdated);
                this.gameContext.Current.DeckCount = deckCount;
            });

            connection.On<Guid, Game>(HubMessages.Game.AllPlayerJoined, (gameId, game) =>
            {
                this.messages.Add(HubMessages.Game.AllPlayerJoined);
                this.gameContext.Current = game;
            });

            connection.On<Guid, string>(HubMessages.Game.PlayerTurn, (gameId, name) =>
            {
                this.messages.Add(HubMessages.Game.PlayerTurn);
                this.gameContext.Current.CurrentPlayerName = name;
            });

            connection.On<Guid, int, string, int>(HubMessages.Game.CardsDrawn, (gameId, gameDeckCount, playerName, playerDeckCount) =>
            {
                this.messages.Add(HubMessages.Game.CardsDrawn);
                this.gameContext.Current.DeckCount = gameDeckCount;
                this.gameContext.Current.Players.Single(p => p.Name == playerName).DeckCount = playerDeckCount;
            });

            await connection.StartAsync();
            this.connection = connection;
        }

        public Task SubscribeToMessagesAsync() =>
            this.connection.InvokeAsync("Subscribe", this.gameContext.Current.Id);

        public async Task CheckMessageAsync(string message)
        {
            await Task.Delay(1000);
            Assert.Contains(message, this.messages);
        }
    }
}
