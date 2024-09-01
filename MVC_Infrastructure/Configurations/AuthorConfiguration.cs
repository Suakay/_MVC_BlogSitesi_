
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Domain.Core.BaseEntityConfigurations;
using MVC_Domain.Entities;

namespace BlogMainStructure.Infrastructure.Configurations
{
    // Configures the entity mapping for Author entity.
    public class AuthorConfiguration : AudiTableEntityConfiguration<Authour>
    {
        public override void Configure(EntityTypeBuilder<Authour> builder)
        {
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(128);

            base.Configure(builder);
        }
    }
}
