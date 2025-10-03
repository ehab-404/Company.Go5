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
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly CompanyDbContext _dbContext;   
        public GenericRepository( CompanyDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public void Add(T model)
        {
            _dbContext.Add(model);
           

        }

        public void Delete(T model)
        {
            _dbContext.Remove(model);
           
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();

        }

        public T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T model)
        {
            _dbContext.Update(model);
           
        }
    }
}
