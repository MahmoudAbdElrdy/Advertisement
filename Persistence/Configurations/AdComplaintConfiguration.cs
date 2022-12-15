using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AdComplaintConfiguration : IEntityTypeConfiguration<AdComplaint> {
    public void Configure(EntityTypeBuilder<AdComplaint> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();

      builder.HasOne(a => a.Client)
        .WithMany(a => a.AdComplaints)
        .HasForeignKey(a => a.ClientId);

       builder.HasOne(a => a.Ad)
        .WithMany(a => a.AdComplaints)
        .HasForeignKey(a => a.AdId);
        }
    }
}