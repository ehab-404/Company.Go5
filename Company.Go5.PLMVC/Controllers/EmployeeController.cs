using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController( IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();

            return View();
        }

        [HttpGet]
        public IActionResult Details(int id) {

            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }
            if(id <= 0)
            {
                return BadRequest("correct id required");
            }




            var employee = _employeeRepository.GetById(id);

            var employeeDto = new EmployeeDto
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Salary = employee.Salary,
                HiringDate = employee.HiringDate
            };




            return View(employeeDto);
        }


        [HttpGet]

        public IActionResult CreateForm()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                    Address = employeeDto.Address,
                    Salary = employeeDto.Salary,
                    HiringDate = employeeDto.HiringDate,
                    CreateAt = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                };
                _employeeRepository.Add(employee);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult EditForm(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = new EmployeeDto
            {
               
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Salary = employee.Salary,
                HiringDate = employee.HiringDate
            };
            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = _employeeRepository.GetById(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }
                existingEmployee.Name = employeeDto.Name;
                existingEmployee.Age = employeeDto.Age;
                existingEmployee.Email = employeeDto.Email;
                existingEmployee.Phone = employeeDto.Phone;
                existingEmployee.Address = employeeDto.Address;
                existingEmployee.Salary = employeeDto.Salary;
                existingEmployee.HiringDate = employeeDto.HiringDate;
                _employeeRepository.Update(existingEmployee);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }



        [HttpGet] public IActionResult DeleteForm()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
