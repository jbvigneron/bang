using Bang.Models.Enums;
using Bang.Models;

namespace Bang.Database.Seeds
{
    public static class WeaponsSeeds
    {
        public static IEnumerable<Weapon> Fill() => new[]
        {
            new Weapon
            {
                Id = WeaponKind.Colt45,
                Name = "Colt .45",
                Range = 1
            },
            new Weapon
            {
                Id = WeaponKind.Volcanic,
                Name = "Volcanic",
                Range = 1
            },
            new Weapon
            {
                Id = WeaponKind.Schofield,
                Name = "Schofield",
                Range = 2
            },
            new Weapon
            {
                Id = WeaponKind.Remington,
                Name = "Remington",
                Range = 3
            },
            new Weapon
            {
                Id = WeaponKind.Carabine,
                Name = "Carabine",
                Range = 4
            },
            new Weapon
            {
                Id = WeaponKind.Winchester,
                Name = "Winchester",
                Range = 5
            },
        };
    }
}
