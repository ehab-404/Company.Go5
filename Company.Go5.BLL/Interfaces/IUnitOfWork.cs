using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Go5.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository employeeRepository { get; }

        public IDepartmentRepository departmentRepository { get; }

        public int Complete();
    }
}
