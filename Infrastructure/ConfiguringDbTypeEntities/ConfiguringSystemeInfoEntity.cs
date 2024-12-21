using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiguringDbTypeEntities
{
    public class ConfiguringSystemeInfoEntity : IEntityTypeConfiguration<SystemeInfo>
    {
        public void Configure(EntityTypeBuilder<SystemeInfo> builder)
        {
            // SystemeInfo properties constraints
            builder
                .Property(si => si.Name)
                .IsRequired()
            .HasMaxLength(100);

            builder
                .Property(si => si.ShortName)
                .IsRequired()
            .HasMaxLength(10);

            builder
                  .Property(si => si.Home)
            .IsRequired();

            builder
                .Property(si => si.AboutUs)
                .IsRequired();
        }
    }
}
