using Microsoft.EntityFrameworkCore;
using Bang.Database.Models;
using Bang.Database.Seeds;

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
        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Character>()
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Character>()
                .Property(b => b.Lives)
                .IsRequired();

            modelBuilder.Entity<Character>()
                .HasData(CharactersSeeds.Data);

            modelBuilder.Entity<Game>()
                .Property(b => b.GameStatus)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(b => b.Role)
                .IsRequired();

            modelBuilder.Entity<Weapon>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Weapon>()
                .Property(b => b.Range)
                .IsRequired();

            modelBuilder.Entity<Weapon>()
                .HasData(WeaponsSeeds.Data);
        }
    }
}