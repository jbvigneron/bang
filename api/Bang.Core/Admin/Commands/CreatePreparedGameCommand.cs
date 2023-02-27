using Bang.Core.Admin.Models;
using MediatR;

namespace Bang.Core.Admin.Commands
{
    public class CreatePreparedGameCommand : IRequest<Guid>
    {
        public CreatePreparedGameCommand(IEnumerable<PlayersInfos> players)
        {
            this.Players = players;
        }

        public IEnumerable<PlayersInfos> Players { get; }
    }
}
