using MediatR;
using Bang.Database.Models;

namespace Bang.Core.Commands
{
    public class CreateGameCommand : IRequest<Game>
    {
        public CreateGameCommand(IEnumerable<string> playerNames)
        {
            PlayerNames = playerNames;
        }

        public IEnumerable<string> PlayerNames { get; }
    }
}
