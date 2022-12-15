using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AuctionConfiguration : IEntityTypeConfiguration<Auction> {
    public void Configure(EntityTypeBuilder<Auction> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.Ad)
        .WithMany(a => a.Auctions)
        .HasForeignKey(a => a.AdId);

    }
  }  
}