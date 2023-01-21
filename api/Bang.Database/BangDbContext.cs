using Bang.Database.Models;
using Bang.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Bang.Database
{
    public class BangDbContext : DbContext
    {
        public BangDbContext(DbContextOptions<BangDbContext> options)
        : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerRole> PlayersRole { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Game>()
                .Property(e => e.GameStatus)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

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