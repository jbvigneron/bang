﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Bang.Database
{
    [ExcludeFromCodeCoverage]
    public class BangDbContextFactory : IDesignTimeDbContextFactory<BangDbContext>
    {
        public BangDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BangDbContext>();
            return new BangDbContext(optionsBuilder.Options);
        }
    }
}