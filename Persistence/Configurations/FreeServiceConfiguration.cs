using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class FreeServiceConfiguration : IEntityTypeConfiguration<FreeService> {
    public void Configure(EntityTypeBuilder<FreeService> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      //builder.HasOne(a => a.Ad)
      //  .WithMany(a => a.FreeServices)
      //  .HasForeignKey(a => a.AdId);
      //builder.HasOne(a => a.ServiceType)
      //  .WithMany(a => a.Services)
      //  .HasForeignKey(a => a.ServiceId);

    }
  }  
}