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

        IEnumerable<T> GetAll();

        T? GetById(int id);

        void Add(T model);

        void Update(T model);

        void Delete(T model  );





    }
}
