using Bang.Core.Commands;
using Bang.Core.Events;
using Bang.Models.Enums;
using MediatR;

namespace Bang.Core.CommandsHandlers
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
                    await this.mediator.Publish(
                        new BrownCardPlay(request.PlayerId, request.Card, request.OpponentId), cancellationToken
                    );
                    break;
                case CardType.Blue:
                    await this.mediator.Publish(
                        new BlueCardPlay(request.PlayerId, request.Card), cancellationToken
                    );
                    break;
                case CardType.Weapon:
                    await this.mediator.Publish(
                        new WeaponCardPlay(request.PlayerId, request.Card), cancellationToken
                    );
                    break;
            }
        }
    }
}
