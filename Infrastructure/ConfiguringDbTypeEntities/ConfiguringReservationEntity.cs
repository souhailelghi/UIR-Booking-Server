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
    public class ConfiguringReservationEntity : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Relationship between Reservation and Sport (many-to-one)
            builder
                .HasOne(r => r.Sport)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
