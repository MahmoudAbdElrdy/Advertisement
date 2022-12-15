using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class SpaceInfoConfiguration : IEntityTypeConfiguration<SpaceInfo> {
    public void Configure(EntityTypeBuilder<SpaceInfo> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.Client)
        .WithMany(a => a.SpaceInfos)
        .HasForeignKey(a => a.ClientId);    
      
      builder.HasOne(a => a.City)
        .WithMany()
        .HasForeignKey(a => a.CityId);


    }
  }
}