using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<CreateGameCommandHandler> logger;

        public CreateGameCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<CreateGameCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var players = request.PlayerNames;

            if (players.Distinct().Count() != players.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents");
            }

            var cards = this.GetCardsOrderedRandomly();
            var game = this.CreateGame(cards);
            this.CreateDeckPile(game, cards);
            this.CreateDiscardPile(game, cards);
            this.AssignPlayersRole(game, players);

            this.dbContext.SaveChanges();

            await this.mediator.Publish(
                new GameCreated(game), cancellationToken
            );

            this.logger.LogInformation("Game created : {@Game}", game);

            return game.Id;
        }

        private ICollection<Card> GetCardsOrderedRandomly()
            => this.dbContext.Cards.OrderBy(c => Guid.NewGuid()).ToArray();

        private Game CreateGame(IEnumerable<Card> cards)
        {
            var game = new Game()
            {
                Status = GameStatus.WaitingForPlayers,
                DeckCount = cards.Count()
            };

            this.dbContext.Games.Add(game);
            return game;
        }

        private void CreateDeckPile(Game game, ICollection<Card> cards)
        {
            var deck = new GameDeck()
            {
                Game = game,
                Cards = cards
            };

            this.dbContext.Decks.Add(deck);
        }

        private void CreateDiscardPile(Game game, ICollection<Card> cards)
        {
            var discard = new GameDiscard()
            {
                Game = game,
                Cards = cards
            };

            this.dbContext.DiscardPiles.Add(discard);
        }

        private void AssignPlayersRole(Game game, IEnumerable<string> players)
        {
            var roles = GetAvailablesRoles(players);

            game.Players = players.Select(playerName =>
            {
                var role = this.GetPlayerRole(roles);

                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatus.NotReady,
                    Role = role,
                };

                if (role.Id == RoleKind.Sheriff)
                {
                    player.IsSheriff = true;
                    game.CurrentPlayerName = player.Name;
                }

                return player;
            }).ToList();
        }

        private static IList<RoleKind> GetAvailablesRoles(IEnumerable<string> players)
        {
            var numberOfPlayers = players.Count();

            if (numberOfPlayers < 4 || numberOfPlayers > 7)
                throw new ArgumentOutOfRangeException(nameof(players), "Le nombre de joueurs doit être compris entre 4 et 7");

            var availableRoles = new List<RoleKind> {
                RoleKind.Sheriff,
                RoleKind.Renegade,
                RoleKind.Outlaw,
                RoleKind.Outlaw
            };

            if (numberOfPlayers >= 5)
                availableRoles.Add(RoleKind.DeputySheriff);

            if (numberOfPlayers >= 6)
                availableRoles.Add(RoleKind.Outlaw);

            if (numberOfPlayers == 7)
                availableRoles.Add(RoleKind.DeputySheriff);

            return availableRoles;
        }

        private Role GetPlayerRole(IList<RoleKind> roles)
        {
            var index = new Random().Next(roles.Count);
            var roleId = roles[index];

            roles.RemoveAt(index);

            return this.dbContext.Roles.Single(r => r.Id == roleId);
        }
    }
}
