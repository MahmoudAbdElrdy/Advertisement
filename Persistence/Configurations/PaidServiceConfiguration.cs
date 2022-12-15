using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class PaidServiceConfiguration : IEntityTypeConfiguration<PaidService> {
    public void Configure(EntityTypeBuilder<PaidService> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


            builder.HasOne(a => a.AdInterval)
              .WithMany(a => a.PaidServices)
              .HasForeignKey(a => a.AdIntervalId);
            builder.HasOne(a => a.Service)
              .WithMany(a => a.PaidServices)
              .HasForeignKey(a => a.ServiceId);

        }
  }  
}