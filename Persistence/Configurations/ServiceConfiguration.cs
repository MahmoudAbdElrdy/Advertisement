using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
  public class ServiceConfiguration : IEntityTypeConfiguration<Service> {
    public void Configure(EntityTypeBuilder<Service> builder) {
      builder.Property(p => p.Id).HasMaxLength(256);
      builder.Property(x => x.Id)
        .HasValueGenerator<SeqIdValueGenerator>()
        .ValueGeneratedOnAdd();
       builder.HasOne(i => i.ServiceType).WithMany(u => u.Services).HasForeignKey(s => s.ServiceTypeId).IsRequired(false);
       builder.HasOne(i => i.User).WithMany(u => u.Services).HasForeignKey(s => s.UserId).IsRequired(false);
             
        }
    }  
}