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
    public class UnitOfWork : IUnitOfWork,IAsyncDisposable
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



        public async Task<int> CompleteAsync()
        {

            return await dbContext.SaveChangesAsync();
        }

        

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();

        }
    }

}
