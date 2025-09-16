using MediatR;
using System;
using System.Collections.Generic;

namespace Bang.Core.Commands
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