using Domain.Entities;
using Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Constants;

namespace Infrastructure.DataAccessManager.EFCore.Configurations
{
    public class ColorConfiguration : BaseEntityConfiguration<Color>
    {
        public override void Configure(EntityTypeBuilder<Color> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(NameConsts.MaxLength).IsRequired(false);
        }
    }
}
