using Bang.Domain.Entities;
using Bang.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Bang.App.Repositories
{
    public interface IGameRepository
    {
        CurrentGame Get(Guid id);

        CurrentGame Create(IEnumerable<Player> players, Player firstPlayer);

        CurrentGame Create(IEnumerable<(string Name, RoleKind RoleId, CharacterKind CharacterId)> players);
    }
}