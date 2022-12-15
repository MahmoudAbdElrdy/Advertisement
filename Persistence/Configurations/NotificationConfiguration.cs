using AuthDomain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class NotificationConfiguration : IEntityTypeConfiguration<Notification> {
    public void Configure(EntityTypeBuilder<Notification> builder) {
      builder.HasKey(a => a.Id);
      builder.Property(a => a.SubjectAr).HasMaxLength(100);
            builder.Property(a => a.SubjectEn).HasMaxLength(100);

            builder.Property(a => a.State).HasMaxLength(50);
      builder.Property(a => a.CreatedBy).HasMaxLength(256);
      builder.Property(a => a.UpdatedBy).HasMaxLength(256);
    }
  }
}