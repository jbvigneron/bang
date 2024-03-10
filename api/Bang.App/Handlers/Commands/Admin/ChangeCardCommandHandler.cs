using Bang.App.Repositories;
using Bang.Domain.Commands.Admin;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Admin
{
    public class ChangeCardCommandHandler : IRequestHandler<ChangeCardCommand>
    {
        private readonly ILogger<ChangeCardCommandHandler> logger;

        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IDeckRepository deckRepository;

        public ChangeCardCommandHandler(
            ILogger<ChangeCardCommandHandler> logger,
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IDeckRepository deckRepository)
        {
            this.logger = logger;

            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
            this.deckRepository = deckRepository;
        }

        public Task Handle(ChangeCardCommand request, CancellationToken cancellationToken)
        {
            var oldCardId = request.OldCardId;
            var newCardName = request.NewCardName;

            var player = this.playerRepository.Get(request.PlayerId);

            var playerHand = this.playerRepository.GetHand(player.Id);
            var oldCard = playerHand.Cards.Single(c => c.Id == oldCardId);

            if (this.deckRepository.IsCardInDeck(player.GameId, newCardName))
            {
                this.logger.LogInformation("Card {OldCardName} is in deck of game {GameId}", oldCard.Name, player.GameId);

                var deck = this.deckRepository.Get(player.GameId);
                var newCard = deck.Cards.First(c => c.Name == newCardName);

                this.deckRepository.SwitchCard(playerHand, deck, oldCard, newCard);
            }
            else
            {
                this.logger.LogInformation("Card {OldCardName} is not present in game deck {GameId}", oldCard.Name, player.GameId);

                var game = this.gameRepository.Get(player.GameId);
                var otherPlayers = game.Players.Where(p => p.Id != player.Id);

                foreach (var otherPlayer in otherPlayers)
                {
                    var hand = this.playerRepository.GetHand(otherPlayer.Id);

                    if (hand.Cards.Any(c => c.Name == newCardName))
                    {
                        this.logger.LogInformation("Card {OldCardName} is in hand of player {PlayerName}", oldCard.Name, player.GameId);

                        var newCard = hand.Cards.First(c => c.Name == newCardName);
                        this.playerRepository.SwitchCard(playerHand, hand, oldCard, newCard);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}