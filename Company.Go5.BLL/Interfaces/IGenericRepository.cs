using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.DAL.Models;

namespace Company.Go5.BLL.Interfaces
{   
    public interface IGenericRepository<T> where T : Entity
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T>? GetByIdAsync(int id);

        Task Add(T model);

        void Update(T model);

        void Delete(T model  );





    }
}
