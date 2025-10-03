using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Company.Go5.DAL.Data.Contexts;

namespace Company.Go5.BLL
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly CompanyDbContext dbContext;

        public IEmployeeRepository employeeRepository { get; }

        public IDepartmentRepository departmentRepository { get; }

        public UnitOfWork(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;

            employeeRepository = new EmployeeRepository(dbContext);
            departmentRepository = new DepartmentRepository(dbContext);

                
        
        
        }



        public int Complete()
        {

            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

    }

}
