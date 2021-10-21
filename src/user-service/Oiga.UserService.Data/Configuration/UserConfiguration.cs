using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oiga.UserService.Data.Entities;

namespace Oiga.UserService.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.FirstName).HasMaxLength(32).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(32).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedAtUtc).IsRequired();
        }
    }
}
