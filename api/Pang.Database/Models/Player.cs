namespace Pang.Database.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public PlayerRole Role { get; set; }
        public int RemainingLifes { get; set; }
        public bool IsAlive => this.RemainingLifes > 0;
    }
}
