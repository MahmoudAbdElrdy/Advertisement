using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AuctionSubiscriberConfiguration : IEntityTypeConfiguration<AuctionSubiscriber> {
    public void Configure(EntityTypeBuilder<AuctionSubiscriber> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd(); 

      builder.HasOne(a => a.Auction)
        .WithMany(a => a.AuctionSubiscribers)
        .HasForeignKey(a => a.AuctionId);

    }
  }  
}