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
    public class ConfiguringTimeRangeEntity : IEntityTypeConfiguration<TimeRange>
    {
        public void Configure(EntityTypeBuilder<TimeRange> builder)
        {
            // TimeRange constraints
            builder
                .Property(t => t.HourStart)
            .IsRequired();

           builder
                .Property(t => t.HourEnd)
                .IsRequired();
        }
    }
}
