using AuthDomain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification> {
    public void Configure(EntityTypeBuilder<UserNotification> builder) {
      builder.HasKey(a => a.Id);
      builder.Property(a => a.UserId).HasMaxLength(256);
      builder.Property(a => a.Read).HasDefaultValue(false);
      builder.Property(a => a.ReadDate).IsRequired(false);

      builder.HasOne(a => a.User)
          .WithMany(a => a.Notifications)
          .HasForeignKey(a => a.UserId);

      builder.HasOne(a => a.Notification)
          .WithMany(a => a.Users)
          .HasForeignKey(a => a.NotficiationId);
    }
  }
}