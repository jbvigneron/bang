using Bang.Domain.Entities;
using Bang.Domain.Enums;
using System.Collections.Generic;

namespace Bang.Persistence.Database.Seeds
{
    public static class RolesSeeds
    {
        public static IEnumerable<Role> Fill() =>
        [
            new Role
            {
                Id = RoleKind.Sheriff,
                Name = "Schérif",
            },
            new Role
            {
                Id = RoleKind.Renegade,
                Name = "Renégat",
            },
            new Role
            {
                Id = RoleKind.Outlaw,
                Name = "Hors-la-loi",
            },
            new Role
            {
                Id = RoleKind.DeputySheriff,
                Name = "Adjoint",
            },
        ];
    }
}