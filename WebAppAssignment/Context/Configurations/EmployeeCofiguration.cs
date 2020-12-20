using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAssignment.Models;

namespace WebAppAssignment.Context.Configurations
{
    public class EmployeeCofiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(t => t.Id);

            builder.HasOne(d => d.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(f => f.DeptId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
