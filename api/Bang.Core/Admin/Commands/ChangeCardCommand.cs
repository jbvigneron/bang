using Bang.Models;
using MediatR;

namespace Bang.Core.Admin.Commands
{
    public class ChangeCardCommand : IRequest
    {
        public ChangeCardCommand()
        {
        }

        public ChangeCardCommand(Guid playerId, Guid oldCardId, string newCardName)
        {
            this.PlayerId = playerId;
            this.OldCardId = oldCardId;
            this.NewCardName = newCardName;
        }

        public Guid PlayerId { get; set; }
        public Guid OldCardId { get; set; }
        public string NewCardName { get; set; }
    }
}
