using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Data.Contexts;
using Company.Go5.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Go5.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly CompanyDbContext _dbContext;   
        public GenericRepository( CompanyDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task Add(T model)
        {
            _dbContext.AddAsync(model);
           

        }

        public void Delete(T model)
        {
            _dbContext.Remove(model);
           
        }

        public async Task<IEnumerable<T> > GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T model)
        {
            _dbContext.Update(model);
           
        }
    }
}
