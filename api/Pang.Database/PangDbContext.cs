using Microsoft.EntityFrameworkCore;
using Pang.Database.Models;

namespace Pang.Database
{
    public class PangDbContext : DbContext
    {
        public PangDbContext(DbContextOptions<PangDbContext> options)
        : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}