using MediatR;
using System;

namespace Bang.Domain.Commands.Admin
{
    public class ChangeCardCommand : IRequest
    {
        public ChangeCardCommand(Guid playerId, Guid oldCardId, string newCardName)
        {
            this.PlayerId = playerId;
            this.OldCardId = oldCardId;
            this.NewCardName = newCardName;
        }

        public Guid PlayerId { get; }
        public Guid OldCardId { get; }
        public string NewCardName { get; }
    }
}