using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Go5.DAL.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Salary).HasColumnType("decimal(18,2)");
            builder.HasOne(p=>p.WorkFor).WithMany(p=>p.employees).
                HasForeignKey(p=>p.WorkForId).OnDelete(DeleteBehavior.SetNull);


        }
    }
}
