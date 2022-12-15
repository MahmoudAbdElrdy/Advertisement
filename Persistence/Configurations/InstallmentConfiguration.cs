using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class InstallmentConfiguration : IEntityTypeConfiguration<Installment> {
    public void Configure(EntityTypeBuilder<Installment> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.AdInterval)
        .WithMany(a => a.Installments)
        .HasForeignKey(a => a.AdIntervalId);

    }
  }  
}