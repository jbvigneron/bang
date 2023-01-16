using Bang.Database.Enums;
using Bang.Database.Models;

namespace Bang.Database.Seeds
{
    public static class WeaponsSeeds
    {
        public static IEnumerable<Weapon> Data { get; } = new Weapon[]
        {
            new Weapon
            {
                Id = WeaponEnum.Colt45,
                Name = "Colt .45",
                Range = 1
            }
        };
    }
}
