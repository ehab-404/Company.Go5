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
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly CompanyDbContext context;

        public DepartmentRepository(CompanyDbContext _context) 
        {
        context = _context;
        }

        public IEnumerable<Department>? GetAll()
        {
            return context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            return context.Departments.Find(x => x.Id == id);

        }

        public int Add(Department department)
        {
          context.Departments.Add(department);
          return context.SaveChanges();
        }

        public int Delete(Department department)
        {
            context.Departments.Delete(department);
            return context.SaveChanges();
        }

       

        public int Update(Department department)
        {
            context.Departments.Update(department);
            return context.SaveChanges();
        }
    }
}
