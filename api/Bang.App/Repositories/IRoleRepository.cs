using Bang.Domain.Entities;
using Bang.Domain.Enums;
using System.Collections.Generic;

namespace Bang.App.Repositories
{
    public interface IRoleRepository
    {
        Role Get(RoleKind id);

        IEnumerable<Role> Get();
    }
}