using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Oiga.Common.Tests
{
    public static class DbContextHelpers
    {
        public static T GetInMemoryDbContext<T>()where T : DbContext
        {
            var provider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInternalServiceProvider(provider)
                .Options;

            var dbContext = (T)Activator.CreateInstance(typeof(T), options);
            dbContext.Database.EnsureDeleted();
            return dbContext;
        }
    }
}
