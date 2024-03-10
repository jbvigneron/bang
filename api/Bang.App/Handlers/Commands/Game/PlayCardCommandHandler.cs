using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Enums;
using Bang.Domain.Extensions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class PlayCardCommandHandler : IRequestHandler<PlayCardCommand>
    {
        private readonly IMediator mediator;

        private readonly IPlayerRepository playerRepository;

        public PlayCardCommandHandler(IMediator mediator, IPlayerRepository playerRepository)
        {
            this.mediator = mediator;
            this.playerRepository = playerRepository;
        }

        public Task Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;
            var cardId = request.CardId;
            var opponentId = request.OpponentId;

            var hand = this.playerRepository.GetHand(user.GetId());
            var card = hand.Cards.Single(c => c.Id == cardId);

            switch (card.Type)
            {
                case CardType.Brown:
                    return this.mediator.Send(
                        new PlayBrownCardCommand(user, card, opponentId), cancellationToken
                    );

                case CardType.Blue:
                    return this.mediator.Send(
                        new PlayBlueCardCommand(user, card, opponentId), cancellationToken
                    );

                case CardType.Weapon:
                    return this.mediator.Send(
                        new PlayWeaponCardCommand(user, card), cancellationToken
                    );

                default:
                    throw new ArgumentOutOfRangeException(nameof(request), "Le type de la carte n'est pas valide");
            }
        }
    }
}