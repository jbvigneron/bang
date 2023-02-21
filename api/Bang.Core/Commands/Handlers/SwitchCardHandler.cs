using Bang.Core.Events;
using MediatR;

namespace Bang.Core.Commands.Handlers
{
    public class SwitchCardHandler : AsyncRequestHandler<SwitchCardCommand>
    {
        private readonly IMediator mediator;

        public SwitchCardHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override Task Handle(SwitchCardCommand request, CancellationToken cancellationToken)
        {
            return mediator.Publish(
                new SwitchCard(request.PlayerId, request.OldCard, request.NewCardName), cancellationToken
            );
        }
    }
}
