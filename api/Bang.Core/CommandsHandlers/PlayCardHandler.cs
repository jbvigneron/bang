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
            if (request.Card.Type == CardType.Weapon)
            {
                await this.mediator.Publish(
                    new ChangeWeapon(request.PlayerId, request.Card), cancellationToken
                );
            }
        }
    }
}
