using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Data.Contexts;
using Company.Go5.DAL.Models;
using Microsoft.EntityFrameworkCore;

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
        
        
        public IEnumerable<Employee> GetAllWithDepartment()
        {
            return _dbContext.Employees.Include(p=>p.WorkFor).ToList();
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employees.Find(id);



        }

        public IEnumerable<Employee> GetAllByName(string name)
        {

            return _dbContext.Employees.Include(p => p.WorkFor).Where(p => p.Name.ToLower().Contains(name.ToLower()));

            
        }

        public Employee GetWithDepartment(int id)
        {
            return _dbContext.Employees.Include(p => p.WorkFor).FirstOrDefault(p => p.Id==id);
        }
        

        public int Update(Employee employee)
        {
            _dbContext.Update(employee);
            return _dbContext.SaveChanges();
        }
    }
}
