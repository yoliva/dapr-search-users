using Microsoft.EntityFrameworkCore;
using Oiga.SearchService.Data.Entities;
using System.Reflection;

namespace Oiga.SearchService.Data
{
    public class SearchServiceDbContext : DbContext
    {
        public SearchServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserData> UsersData { get; set; }
    }
}
