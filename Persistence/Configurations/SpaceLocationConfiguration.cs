using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class SpaceLocationConfiguration : IEntityTypeConfiguration<SpaceLocation> {
        public void Configure(EntityTypeBuilder<SpaceLocation> builder) {
            builder.HasOne(i => i.Space).WithOne(u => u.Location).HasForeignKey<SpaceLocation>(s => s.SpaceInfoId).IsRequired(false);
        }
    }   
}