using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Go5.DAL.Models;

namespace Company.Go5.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();

        //Employee? GetById(int id);

        //int Add (Employee employee);

        //int Update (Employee employee);

        //int Delete (Employee employee);

        public Task<IEnumerable<Employee>> GetAllWithDepartmentAsync();

        public Task<Employee> GetWithDepartmentAsync(int id );


        public Task<IEnumerable<Employee>> GetAllByNameAsync(string name);



    }
}
