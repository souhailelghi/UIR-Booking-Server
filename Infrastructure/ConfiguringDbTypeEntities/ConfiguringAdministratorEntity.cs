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
    public class ConfiguringAdministratorEntity : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            // Configure Administrator-specific properties
           builder
                .Property(a => a.AdminName)
                .IsRequired();
        }
    }
}
