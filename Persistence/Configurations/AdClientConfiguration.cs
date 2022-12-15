using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AdClientConfiguration : IEntityTypeConfiguration<AdClient> {
    public void Configure(EntityTypeBuilder<AdClient> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.Ad)
        .WithMany(a => a.AdClients)
        .HasForeignKey(a => a.AdId);

    }
  }  
}