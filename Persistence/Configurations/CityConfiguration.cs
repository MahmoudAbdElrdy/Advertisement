using AuthDomain.Entities;
using AuthDomain.Entities.Auth;
using Common.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueConverters;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class CityConfiguration : IEntityTypeConfiguration<City> {
    public void Configure(EntityTypeBuilder<City> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();
      builder.HasOne(a => a.Country)
        .WithMany(a => a.Cities)
        .HasForeignKey(a => a.CountryId);

    }
  }
}