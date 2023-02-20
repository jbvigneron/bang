using Bang.Core.Constants;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers.Hubs
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
            var server = httpClientFactoryContext.Factory.Server;
            var connection = SignalRHelper.ConnectToOpenHub(server, "http://localhost/GameHub");

            connection.On<Guid, int>(HubMessages.Game.GameDeckReady, (gameId, deckCount) =>
            {
                messages.Add(HubMessages.Game.GameDeckReady);
                gameContext.Current.DeckCount = deckCount;
            });

            connection.On<Guid, Player>(HubMessages.Game.PlayerJoin, (gameId, player) =>
            {
                messages.Add(HubMessages.Game.PlayerJoin);
                var players = gameContext.Current.Players;

                for (int i = 0; i < players.Count; i++)
                    if (players[i].Id == player.Id)
                        players[i] = player;
            });

            connection.On<Guid, int>(HubMessages.Game.DeckUpdated, (gameId, deckCount) =>
            {
                messages.Add(HubMessages.Game.DeckUpdated);
                gameContext.Current.DeckCount = deckCount;
            });

            connection.On<Guid, Game>(HubMessages.Game.AllPlayerJoined, (gameId, game) =>
            {
                messages.Add(HubMessages.Game.AllPlayerJoined);
                gameContext.Current = game;
            });

            connection.On<Guid, string>(HubMessages.Game.PlayerTurn, (gameId, name) =>
            {
                messages.Add(HubMessages.Game.PlayerTurn);
                gameContext.Current.CurrentPlayerName = name;
            });

            connection.On<Guid, int, string, int>(HubMessages.Game.CardsDrawn, (gameId, gameDeckCount, playerName, cardsInHand) =>
            {
                messages.Add(HubMessages.Game.CardsDrawn);
                gameContext.Current.DeckCount = gameDeckCount;
                gameContext.Current.Players.Single(p => p.Name == playerName).CardsInHand = cardsInHand;
            });

            await connection.StartAsync();
            this.connection = connection;
        }

        public Task SubscribeToMessagesAsync() =>
            connection.InvokeAsync("Subscribe", gameContext.Current.Id);

        public async Task CheckMessageAsync(string message)
        {
            await Task.Delay(1500);
            Assert.Contains(message, messages);
        }
    }
}
