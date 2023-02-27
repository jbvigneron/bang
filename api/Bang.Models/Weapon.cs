using Bang.Models.Enums;

namespace Bang.Models
{
    public class Weapon
    {
        public WeaponKind Id { get; set; }
        public string? Name { get; set; }
        public int Range { get; set; }
    }
}
