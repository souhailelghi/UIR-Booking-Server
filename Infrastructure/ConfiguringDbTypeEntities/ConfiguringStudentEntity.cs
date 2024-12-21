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
    public class ConfiguringStudentEntity : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Configure Student-specific properties
            builder
                .Property(s => s.UserId)
                .IsRequired();
            // CodeUIR unique for the Student entity
            builder
                .HasIndex(s => s.CodeUIR)
                .IsUnique();
        }
    }
}
