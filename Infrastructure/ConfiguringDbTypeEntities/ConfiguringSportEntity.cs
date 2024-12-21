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
    public class ConfiguringSportEntity : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            // Relationship between Sport and SportCategory (many-to-one)
            builder
                .HasOne<SportCategory>()
                .WithMany()
                .HasForeignKey(s => s.CategorieId)
                .OnDelete(DeleteBehavior.Cascade);


            //Sport entity constraints
          builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(s => s.ReferenceSport)
                .IsRequired();

            builder
                 .Property(s => s.NbPlayer)
                .IsRequired();
        }
    }
}
