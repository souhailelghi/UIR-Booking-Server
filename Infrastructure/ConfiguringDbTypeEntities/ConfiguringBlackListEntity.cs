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
    public class ConfiguringBlackListEntity : IEntityTypeConfiguration<BlackList>
    {
        public void Configure(EntityTypeBuilder<BlackList> builder)
        {
            // Relationship between BlackList and Reservation (many-to-one)
            builder
                .HasOne<Reservation>()
                .WithMany()
                .HasForeignKey(b => b.ReservationId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure BlackList entries are deleted when a Reservation is deleted
        }
    }
}
