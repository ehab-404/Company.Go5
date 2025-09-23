using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Go5.DAL.Data.Contexts
{
    public class CompanyDbContext:DbContext
    {

        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        






        


    }
}
