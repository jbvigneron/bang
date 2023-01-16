using Bang.Database.Models;
using MediatR;

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
