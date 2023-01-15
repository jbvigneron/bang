namespace Bang.Database.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Character? Character { get; set; }
        public int Lives { get; set; }
        public bool IsAlive => this.Lives > 0;
        public PlayerRole? Role { get; set; }
        public bool IsScheriff { get; set; }
        public Weapon Weapon { get; set; }
    }
}
