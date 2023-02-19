using Bang.Models.Enums;

namespace Bang.Models
{
    public class Character
    {
        public CharacterKind Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lives { get; set; }
    }
}
