using Bang.Database.Enums;

namespace Bang.Database.Models
{
    public class Weapon
    {
        public WeaponEnum Id { get; set; }
        public string Name { get; set; }
        public int Range { get; set; }
    }
}
