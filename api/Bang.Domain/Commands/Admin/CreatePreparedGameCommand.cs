using Bang.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Bang.Domain.Commands.Admin
{
    public class CreatePreparedGameCommand : IRequest<Guid>
    {
        public CreatePreparedGameCommand(IEnumerable<PlayersInfos> players)
        {
            this.Players = players;
        }

        public IEnumerable<PlayersInfos> Players { get; }

        public class PlayersInfos
        {
            public PlayersInfos()
            {
                this.Name = string.Empty;
            }

            public string Name { get; set; }
            public CharacterKind CharacterId { get; set; }
            public RoleKind RoleId { get; set; }
        }
    }
}