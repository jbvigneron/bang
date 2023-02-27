using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Admin.Commands.Handlers
{
    public class ChangeCardCommandHandler : IRequestHandler<ChangeCardCommand>
    {
        private readonly BangDbContext dbContext;

        public ChangeCardCommandHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ChangeCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.PlayerId;
            var oldCardId = request.OldCardId;
            var newCardName = request.NewCardName;

            var playerHand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == playerId, cancellationToken);

            var oldCard = playerHand.Cards!.Single(c => c.Id == oldCardId);

            Card newCard;
            ICollection<Card> sourceDeck;

            if (this.IsCardInGameDeck(playerHand, newCardName))
            {
                var gameDeck = await this.dbContext.GamesDecks
                    .Include(d => d.Cards)
                    .Include(d => d.Game)
                    .FirstAsync(d => d.GameId == playerHand.Player!.GameId && d.Cards!.Any(c => c.Name == newCardName), cancellationToken);

                newCard = gameDeck.Cards!.First(c => c.Name == newCardName);
                sourceDeck = gameDeck.Cards!;
            }
            else
            {
                var otherPlayerHand = await this.dbContext.PlayersHands
                    .Include(d => d.Cards)
                    .Include(d => d.Player)
                    .FirstAsync(d => d.PlayerId != playerId && d.Cards!.Any(c => c.Name == newCardName), cancellationToken);

                newCard = otherPlayerHand.Cards!.First(c => c.Name == newCardName);
                sourceDeck = otherPlayerHand.Cards!;
            }

            sourceDeck!.Add(oldCard);
            playerHand.Cards!.Add(newCard);

            sourceDeck.Remove(newCard);
            playerHand.Cards.Remove(oldCard);

            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        private bool IsCardInGameDeck(PlayerHand hand, string cardName) =>
            this.dbContext.GamesDecks.Any(d => d.GameId == hand.Player!.GameId && d.Cards!.Any(c => c.Name == cardName));
    }
}
