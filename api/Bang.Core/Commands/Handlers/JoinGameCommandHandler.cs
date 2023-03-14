using Bang.Core.Exceptions;
using Bang.Core.Notifications;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Guid>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<JoinGameCommandHandler> logger;

        public JoinGameCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<JoinGameCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Guid> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = request.GameId;
            var playerName = request.PlayerName;

            this.logger.LogInformation("{PlayerName} wants to join game {GameId}", gameId);

            var game = this.GetGame(gameId);
            var player = game.Players!.First(p => p.Name == playerName);
            this.SetPlayerInfos(player);
            this.FillPlayerHand(player);
            UpdateGameStatus(game);

            this.dbContext.SaveChanges();

            await this.mediator.Publish(
                new PlayerJoined(game, player), cancellationToken
            );

            this.logger.LogInformation("{@Player} has joined game {GameId}", player, gameId);

            return player.Id;
        }

        private Game GetGame(Guid gameId)
            => this.dbContext.Games
                .Include(g => g.Players)
                .First(g => g.Id == gameId);

        private static void UpdateGameStatus(Game game)
        {
            if (game.Status != GameStatus.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect", game.Id);
            }

            if (game.Players!.All(p => p.Status == PlayerStatus.Alive))
            {
                game.Status = GameStatus.InProgress;
            }
        }

        private void SetPlayerInfos(Player player)
        {
            player.Character = this.GetRandomCharacter();
            player.Lives = GetLives(player.Character, player.IsSheriff);
            player.Weapon = this.GetColt45();
            player.Status = PlayerStatus.Alive;
        }

        private Character GetRandomCharacter()
            => this.dbContext.Characters.OrderBy(c => Guid.NewGuid()).First();

        private static int GetLives(Character character, bool isScheriff)
        {
            var lives = character.Lives;
            lives += isScheriff ? 1 : 0;
            return lives;
        }

        private Weapon GetColt45()
            => this.dbContext.Weapons.Single(w => w.Id == WeaponKind.Colt45);

        private void FillPlayerHand(Player player)
        {
            var hand = new PlayerHand()
            {
                Player = player,
                Cards = new List<Card>()
            };

            var gameDeck = this.dbContext.Decks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .First(d => d.Game!.Players!.Any(p => p.Id == player.Id));

            for (int i = 1; i <= hand.Player!.Lives; i++)
            {
                var card = gameDeck.Cards!.First();

                hand.Cards!.Add(card);
                hand.Player.CardsInHand++;

                gameDeck.Cards!.Remove(card);
                gameDeck.Game!.DeckCount--;
            }

            this.dbContext.PlayersHands.Add(hand);
        }
    }
}
