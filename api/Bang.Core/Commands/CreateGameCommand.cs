using Bang.Models;
using MediatR;

namespace Bang.Core.Commands
{
    public class CreateGameCommand : IRequest<Guid>
    {
        public CreateGameCommand(IEnumerable<string> playerNames)
        {
            PlayerNames = playerNames;
        }

        public IEnumerable<string> PlayerNames { get; }
    }
}
