using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class SpaceImageConfiguration : IEntityTypeConfiguration<SpaceImage> {
        public void Configure(EntityTypeBuilder<SpaceImage> builder) {
            builder.HasOne(i => i.Space).WithMany(u => u.Images).HasForeignKey(s => s.SpaceInfoId).IsRequired(false);
        }
    } 
}