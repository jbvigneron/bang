using Bang.Domain.Enums;

namespace Bang.Domain.Entities
{
    public class Weapon
    {
        public WeaponKind Id { get; set; }
        public string Name { get; set; }
        public int Range { get; set; }
    }
}
