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

        //public int Add(Employee employee)
        //{
        //    _dbContext.Add(employee);
           

        //    return _dbContext.SaveChanges();



        //}

        //public int Delete(Employee employee)
        //{
        //    employee.WorkFor = null;
        //    employee.WorkForId = null;
        //    _dbContext.Remove(employee);
        //    return _dbContext.SaveChanges();
        //}

        public async Task< IEnumerable<Employee> > GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }
        
        
        public  async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync()
        {
            return await _dbContext.Employees.Include(p=>p.WorkFor).ToListAsync();
        }

        public async Task< Employee? > GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);



        }

        public async Task<IEnumerable<Employee>> GetAllByNameAsync(string name)
        {
            return await _dbContext.Employees.Include(p=>p.WorkFor).Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task< Employee> GetWithDepartmentAsync(int id)
        {
            return await _dbContext.Employees.Include(p => p.WorkFor).FirstOrDefaultAsync(p => p.Id==id);
        }
        

        //public int Update(Employee employee)
        //{
        //    _dbContext.Update(employee);
        //    return _dbContext.SaveChanges();
        //}
    }
}
