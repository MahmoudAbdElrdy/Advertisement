using AuthDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class TransactionConfiguration : IEntityTypeConfiguration<Transaction> {
    public void Configure(EntityTypeBuilder<Transaction> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();


      builder.HasOne(a => a.User)
        .WithMany(a => a.Transactions)
        .HasForeignKey(a => a.UserId);
      
      builder.HasOne(a => a.Ad)
        .WithMany(a => a.Transactions)
        .HasForeignKey(a => a.AdId)
        .IsRequired(false)
        ;

    }
  }
}