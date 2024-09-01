
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Domain.Core.BaseEntityConfigurations;
using MVC_Domain.Entities;

namespace BlogMainStructure.Infrastructure.Configurations
{
    public class ArticleConfiguration : AudiTableEntityConfiguration<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(256);
            base.Configure(builder);
        }
    }
}
