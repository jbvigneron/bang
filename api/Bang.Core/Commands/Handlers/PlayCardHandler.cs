using Bang.Core.Events;
using Bang.Models.Enums;
using MediatR;

namespace Bang.Core.Commands.Handlers
{
    public class PlayCardHandler : AsyncRequestHandler<PlayCardCommand>
    {
        private readonly IMediator mediator;

        public PlayCardHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override async Task Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            switch (request.Card.Type)
            {
                case CardType.Brown:
                    await mediator.Publish(
                        new BrownCardPlay(request.PlayerId, request.Card, request.OpponentId), cancellationToken
                    );
                    break;
                case CardType.Blue:
                    await mediator.Publish(
                        new BlueCardPlay(request.PlayerId, request.Card), cancellationToken
                    );
                    break;
                case CardType.Weapon:
                    await mediator.Publish(
                        new WeaponCardPlay(request.PlayerId, request.Card), cancellationToken
                    );
                    break;
            }

        }
    }
}
