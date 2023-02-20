using Bang.Core.Commands;
using Bang.Core.Events;
using MediatR;

namespace Bang.Core.CommandsHandlers
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
            return this.mediator.Publish(
                new SwitchCard(request.PlayerId, request.OldCard, request.NewCardName), cancellationToken
            );
        }
    }
}
