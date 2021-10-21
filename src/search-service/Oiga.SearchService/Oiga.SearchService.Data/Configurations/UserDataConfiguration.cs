using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oiga.SearchService.Data.Entities;

namespace Oiga.SearchService.Data.Configurations
{
    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.FullName).HasMaxLength(64).IsRequired().UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired().UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
