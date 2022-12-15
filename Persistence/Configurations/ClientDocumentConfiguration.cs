using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class ClientDocumentConfiguration : IEntityTypeConfiguration<ClientDocument> {
        public void Configure(EntityTypeBuilder<ClientDocument> builder) {
            builder.HasOne(i => i.Client).WithMany(u => u.Documents).HasForeignKey(s => s.ClientId).IsRequired(false);
        }
    }
}