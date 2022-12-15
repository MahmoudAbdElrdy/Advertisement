using AuthDomain.Entities;
using AuthDomain.Entities.Auth;
using Common.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueConverters;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
    public class CountryConfiguration : IEntityTypeConfiguration<Country> {
    public void Configure(EntityTypeBuilder<Country> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();

     }
  }
}