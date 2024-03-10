using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using Bang.Domain.Events;
using Bang.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Guid>
    {
        private readonly ILogger<JoinGameCommandHandler> logger;
        private readonly IMediator mediator;

        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly ICharactersRepository charactersRepository;
        private readonly IWeaponsRepository weaponsRepository;
        private readonly IDeckRepository deckRepository;

        public JoinGameCommandHandler(
            ILogger<JoinGameCommandHandler> logger,
            IMediator mediator,
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            ICharactersRepository charactersRepository,
            IWeaponsRepository weaponsRepository,
            IDeckRepository deckRepository)
        {
            this.logger = logger;
            this.mediator = mediator;

            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
            this.charactersRepository = charactersRepository;
            this.weaponsRepository = weaponsRepository;
            this.deckRepository = deckRepository;
        }

        public async Task<Guid> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = request.GameId;
            var playerName = request.PlayerName;
            this.logger.LogInformation("Player {PlayerName} wants to join game {GameId}", gameId);

            var game = this.gameRepository.Get(gameId);

            if (game.Status != GameStatus.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect", game.Id);
            }

            var player = game.Players.First(p => p.Name == playerName);
            player.Character = this.charactersRepository.GetRandom();
            player.Lives = this.GetLives(player.Character, player.IsSheriff);
            player.Weapon = this.weaponsRepository.GetFirstWeapon();
            player.Status = PlayerStatus.Alive;

            var gameDeck = this.deckRepository.Get(gameId);
            var canStartGame = gameDeck.Game.Players.All(p => p.Status == PlayerStatus.Alive);
            this.playerRepository.FillPlayerHand(gameDeck, player, canStartGame);

            await this.mediator.Publish(
                new PlayerJoined(game, player), cancellationToken
            );

            this.logger.LogInformation("Player {@Player} has joined game {GameId}", player, gameId);

            return player.Id;
        }

        private int GetLives(Character character, bool isScheriff)
        {
            var lives = character.Lives;
            lives += isScheriff ? 1 : 0;
            return lives;
        }
    }
}