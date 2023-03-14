using Bang.Core.Extensions;
using Bang.Database;
using Bang.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Commands.Handlers
{
    public class PlayCardCommandHandler : IRequestHandler<PlayCardCommand>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;

        public PlayCardCommandHandler(BangDbContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
        }

        public Task Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;
            var cardId = request.CardId;
            var opponentId = request.OpponentId;

            var hand = this.dbContext.PlayersHands
                .Include(h => h.Cards)
                .Single(h => h.PlayerId == user.GetId());

            var card = hand.Cards!.Single(c => c.Id == cardId);

            return card.Type switch
            {
                CardType.Brown =>
                    this.mediator.Send(
                        new PlayBrownCardCommand(user, card, opponentId), cancellationToken
                    ),
                CardType.Blue =>
                    this.mediator.Send(
                        new PlayBlueCardCommand(user, card, opponentId), cancellationToken
                    ),
                CardType.Weapon =>
                this.mediator.Send(
                        new PlayWeaponCardCommand(user, card), cancellationToken
                    ),
                _ => throw new ArgumentOutOfRangeException(nameof(request), "Le type de la carte n'est pas valide"),
            };
        }
    }
}
