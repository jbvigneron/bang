using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bang.Database
{
    public class BangDbContextFactory : IDesignTimeDbContextFactory<BangDbContext>
    {
        public BangDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BangDbContext>();
            //optionsBuilder.UseSqlite("Data Source=../bang.db");

            return new BangDbContext(optionsBuilder.Options);
        }
    }
}