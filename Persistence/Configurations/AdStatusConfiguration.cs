using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class AdStatusConfiguration : IEntityTypeConfiguration<AdStatus> {
        public void Configure(EntityTypeBuilder<AdStatus> builder) {
            builder.Property(p => p.Id).HasMaxLength(256);
            builder.Property(x => x.Id)
              .HasValueGenerator<SeqIdValueGenerator>()
              .ValueGeneratedOnAdd();
            builder.HasOne(i => i.Ad).WithMany(u => u.AdStatuses).HasForeignKey(s => s.AdId).IsRequired(false);
        }
    } 
}