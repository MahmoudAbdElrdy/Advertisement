using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AdConfiguration : IEntityTypeConfiguration<Ad> {
    public void Configure(EntityTypeBuilder<Ad> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.SpaceInfo)
        .WithMany(a => a.Ads)
        .HasForeignKey(a => a.SpaceInfoId);

    }
  }
}