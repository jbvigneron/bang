using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class DrawCardsCommandHandler : IRequestHandler<DrawCardsCommand>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<DrawCardsCommandHandler> logger;

        public DrawCardsCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<DrawCardsCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(DrawCardsCommand request, CancellationToken cancellationToken)
        {
            var game = this.GetGame(request.GameId);
            var player = game.Players!.First(p => p.Id == request.PlayerId);

            CheckCurrentStatus(game, player);
            var cards = DrawCards(game, player);

            this.dbContext.SaveChanges();

            await this.mediator.Publish(
                new CardsDrawn(game, player, cards), cancellationToken
            );

            this.logger.LogInformation("Player {@Player} draw cards", player);
        }

        private Game GetGame(Guid gameId)
            => this.dbContext.Games
                .Include(g => g.Players)
                .First(g => g.Id == gameId);

        private static void CheckCurrentStatus(Game game, Player player)
        {
            if (player.HasDrawnCards)
            {
                throw new PlayerException("Vous avez déjà pioché vos cartes.", player);
            }

            if (game.Status == GameStatus.WaitingForPlayers)
            {
                throw new GameException("La partie n'a pas encore démarré.", game);
            }

            if (game.Status == GameStatus.Finished)
            {
                throw new GameException("La partie est terminée.", game);
            }

            if (game.CurrentPlayerName != player.Name)
            {
                throw new PlayerException("Ce n'est pas à vous de jouer.", player);
            }
        }

        private IEnumerable<Card> DrawCards(Game game, Player player)
        {
            var hand = this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Single(p => p.PlayerId == player.Id);

            var deck = this.dbContext.Decks
                .Include(d => d.Cards)
                .Single(d => d.GameId == game.Id);

            for (var i = 1; i <= 2; i++)
            {
                var card = deck.Cards!.First();

                hand.Cards!.Add(card);
                player!.CardsInHand++;

                deck.Cards!.Remove(card);
                game!.DeckCount--;
            }

            player!.HasDrawnCards = true;
            return hand.Cards!;
        }
    }
}
