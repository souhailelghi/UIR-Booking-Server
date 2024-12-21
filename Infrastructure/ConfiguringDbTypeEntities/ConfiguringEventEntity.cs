using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiguringDbTypeEntities
{
    public class ConfiguringEventEntity : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //Event properties constraints
           builder
                .Property(s => s.Title)
                .IsRequired()
            .HasMaxLength(100);

            builder
                .Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
