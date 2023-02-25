using Bang.Core.Constants;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers.Technical.Hubs
{
    public class GameHubDriver
    {
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        private HubConnection? connection;
        private readonly IList<string> messages = new List<string>();

        public GameHubDriver(GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = httpClientFactoryContext.Factory!.Server;

            this.connection = HubHelper.ConnectToOpenHub(server, "http://localhost/GameHub");

            this.connection.On<Guid, Player>(HubMessages.Game.PlayerJoin, (gameId, player) =>
            {
                this.messages.Add(HubMessages.Game.PlayerJoin);
                var players = gameContext.Current.Players;

                for (int i = 0; i < players.Count; i++)
                    if (players[i].Id == player.Id)
                        players[i] = player;
            });

            this.connection.On<Guid, int>(HubMessages.Game.DeckUpdated, (gameId, deckCount) =>
            {
                this.messages.Add(HubMessages.Game.DeckUpdated);
                gameContext.Current.DeckCount = deckCount;
            });

            this.connection.On<Guid, Game>(HubMessages.Game.AllPlayerJoined, (gameId, game) =>
            {
                this.messages.Add(HubMessages.Game.AllPlayerJoined);
                gameContext.Current = game;
            });

            this.connection.On<Guid, string>(HubMessages.Game.PlayerTurn, (gameId, name) =>
            {
                this.messages.Add(HubMessages.Game.PlayerTurn);
                gameContext.Current.CurrentPlayerName = name;
            });

            this.connection.On<Guid, int, string, int>(HubMessages.Game.CardsDrawn, (gameId, gameDeckCount, playerName, cardsInHand) =>
            {
                this.messages.Add(HubMessages.Game.CardsDrawn);
                gameContext.Current.DeckCount = gameDeckCount;
                gameContext.Current.Players.Single(p => p.Name == playerName).CardsInHand = cardsInHand;
            });

            await connection.StartAsync();
        }

        public Task SubscribeToMessagesAsync() =>
            this.connection!.InvokeAsync("Subscribe", gameContext.Current.Id);

        public void CheckMessage(string message) =>
            HubHelper.CheckMessages(this.messages, message);
    }
}
