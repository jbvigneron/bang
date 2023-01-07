using MediatR;
using Pang.Database.Models;

namespace Pang.Core.Commands
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
