using Bang.Domain.Entities;
using Bang.Persistence.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Bang.Persistence.Database
{
    public class BangDbContext : DbContext
    {
        public BangDbContext(DbContextOptions<BangDbContext> options)
        : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CurrentGame> Games { get; set; }
        public DbSet<GameDeck> Decks { get; set; }
        public DbSet<GameDiscard> DiscardPiles { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerHand> PlayersHands { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .Property(e => e.Id);

            modelBuilder.Entity<Card>()
                .HasData(CardsSeeds.Fill());

            modelBuilder.Entity<Character>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Character>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Character>()
                .Property(e => e.Lives)
                .IsRequired();

            modelBuilder.Entity<Character>()
                .HasData(CharactersSeeds.Fill());

            modelBuilder.Entity<CurrentGame>()
                .Property(e => e.Status)
                .IsRequired();

            modelBuilder.Entity<CurrentGame>()
                .HasMany(g => g.Players)
                .WithOne()
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDeck>()
                .HasOne(e => e.Game)
                .WithMany()
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDeck>()
                .HasMany(e => e.Cards)
                .WithMany();

            modelBuilder.Entity<GameDiscard>()
                .HasOne(e => e.Game)
                .WithMany()
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDiscard>()
                .HasMany(e => e.Cards)
                .WithMany();

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .HasOne(e => e.Character)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(e => e.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                .HasOne(e => e.Weapon)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerHand>()
                .HasOne(e => e.Player)
                .WithMany()
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerHand>()
                .HasMany(e => e.Cards)
                .WithMany();

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasData(RolesSeeds.Fill());

            modelBuilder.Entity<Weapon>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Weapon>()
                .Property(e => e.Range)
                .IsRequired();

            modelBuilder.Entity<Weapon>()
                .HasData(WeaponsSeeds.Fill());
        }
    }
}