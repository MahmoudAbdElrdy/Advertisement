using AuthDomain.Entities.Auth;
using Common.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueConverters;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();

      builder.Ignore(p => p.FullName);
    }
  }
}