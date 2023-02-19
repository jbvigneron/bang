using Bang.Models;
using Bang.Models.Enums;

namespace Bang.Database.Seeds
{
    public static class RolesSeeds
    {
        public static IEnumerable<Role> Fill() => new[]
        {
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
        };
    }
}
