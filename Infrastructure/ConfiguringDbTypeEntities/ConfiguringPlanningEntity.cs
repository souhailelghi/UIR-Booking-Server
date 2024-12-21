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
    public class ConfiguringPlanningEntity : IEntityTypeConfiguration<Planning>
    {
        public void Configure(EntityTypeBuilder<Planning> builder)
        {
            // Relationship between Planning and Sport (many-to-one)
            builder
                .HasOne<Sport>()
                .WithMany()
                .HasForeignKey(p => p.SportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship between Planning and TimeRange (one-to-many)
            builder
                .HasMany(p => p.TimeRanges)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
