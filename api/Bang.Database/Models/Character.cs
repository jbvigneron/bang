using Bang.Database.Enums;

namespace Bang.Database.Models
{
    public class Character
    {
        public CharacterEnum Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lives { get; set; }
    }
}
