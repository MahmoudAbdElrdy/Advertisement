using AuthDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class UserCodeConfiguration : IEntityTypeConfiguration<UserCode> {
    public void Configure(EntityTypeBuilder<UserCode> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.User)
        .WithMany(a => a.Codes)
        .HasForeignKey(a => a.UserId);

    }
  }
}