namespace Bang.Database.Models
{
    public class Character
    {
        public CharacterId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lives { get; set; }
    }
}
