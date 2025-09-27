using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Data.Contexts;
using Company.Go5.DAL.Models;

namespace Company.Go5.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _dbContext;

        public EmployeeRepository(CompanyDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }

        public int Add(Employee employee)
        {
            _dbContext.Add(employee);
           

            return _dbContext.SaveChanges();



        }

        public int Delete(Employee employee)
        {
            _dbContext.Remove(employee);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employees.Find(id);



        }

        public int Update(Employee employee)
        {
            _dbContext.Update(employee);
            return _dbContext.SaveChanges();
        }
    }
}
