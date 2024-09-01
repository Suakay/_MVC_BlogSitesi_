using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Core.BaseEntityConfigurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();  //BaseEntitye new guid yazdığımızda buna gerek yok ama buradan da
                                                                //yönetilebildiğini gormemiz için yazdık.
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true); // true yazmak zorunda degilsin defaultu true zaten.
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.UpdatedBy).IsRequired(false);
            builder.Property(x => x.UpdatedDate).IsRequired(false);

        }


    }
}
