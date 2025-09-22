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

        public CompanyDbContext() : base()
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }

        DbSet<Department> Departments {  get; set; }    
        


    }
}
