using AuthDomain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations {
  public class UserAvatarConfiguration : IEntityTypeConfiguration<UserAvatar> {
        public void Configure(EntityTypeBuilder<UserAvatar> builder) {
            builder.HasOne(i => i.User).WithOne(u => u.Avatar).HasForeignKey<UserAvatar>(s => s.UserId).IsRequired(false);
        }
    }
}