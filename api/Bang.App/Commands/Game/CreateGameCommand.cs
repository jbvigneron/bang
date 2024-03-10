using MediatR;
using System;
using System.Collections.Generic;

namespace Bang.App.Commands.Game
{
    public sealed class CreateGameCommand : IRequest<Guid>
    {
        public CreateGameCommand(IEnumerable<string> playerNames)
        {
            this.PlayerNames = playerNames;
        }

        public IEnumerable<string> PlayerNames { get; }
    }
}