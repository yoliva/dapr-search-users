using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Oiga.UserService.Data.Entities
{
    public class UsersServiceDbContext : DbContext
    {
        public UsersServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; set; }
    }
}
