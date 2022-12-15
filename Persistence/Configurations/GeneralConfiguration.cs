using AuthDomain.Entities.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.ValueGenerators;

namespace Persistence.Configurations {
    public class GeneralConfigurationConfiguration : IEntityTypeConfiguration<GeneralConfiguration>
    {
        public void Configure(EntityTypeBuilder<GeneralConfiguration> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}