using MediatR;

namespace Bang.Core.Commands
{
    public class DrawCardsCommand : IRequest
    {
        public DrawCardsCommand(Guid playerId)
        {
            this.PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
