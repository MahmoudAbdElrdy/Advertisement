using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class OrderComplaintConfiguration : IEntityTypeConfiguration<OrderComplaint> {
    public void Configure(EntityTypeBuilder<OrderComplaint> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();

      builder.HasOne(a => a.Client)
        .WithMany(a => a.OrderComplaints)
        .HasForeignKey(a => a.ClientId);

       builder.HasOne(a => a.AdInterval)
        .WithMany(a => a.OrderComplaints)
        .HasForeignKey(a => a.OrderId);
        }
    }
}