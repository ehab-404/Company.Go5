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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {

        private readonly CompanyDbContext context;

        //ask clr to create object of type CompanyDbContext
        //when assigning it reference of same type of CompanyDbContext
        public DepartmentRepository(CompanyDbContext _context) :base(_context)
        {
        context = _context;
        }

        public IEnumerable<Department>? GetAll()
        {
            return context.Departments.ToList();
        }

        public Department? GetById(int id)
        {

            return context.Departments.Find(id);

        }

        //public int Add(Department department)
        //{
        //    context.Departments.Add(department);
        //    return context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    context.Departments.Remove(department);
        //    return context.SaveChanges();
        //}



        //public int Update(Department department)
        //{
        //   context.Departments.Update(department);
        //    return context.SaveChanges();
        //}
    }
}
