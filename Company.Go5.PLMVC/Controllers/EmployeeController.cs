using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.Go5.PLMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeController( IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository   )
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index(string? SearchInput)
        {
            var employees = _employeeRepository.GetAllWithDepartment();

            if (!string.IsNullOrEmpty(SearchInput))
            {

                employees = _employeeRepository.GetAllByName(SearchInput);
            }

            return View(employees);
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




            var employee = _employeeRepository.GetWithDepartment(id);



            return View(employee);
        }


        [HttpGet]

        public IActionResult CreateForm()
        {


            ViewBag.Departments = _departmentRepository.GetAll();

            

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
                    IsDeleted = false,
                    WorkForId = employeeDto.WorkForId
                };
                _employeeRepository.Add(employee);
            }

            else { return BadRequest(ModelState); }
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
                HiringDate = employee.HiringDate,
                WorkFor = employee.WorkFor,
                WorkForId = employee.WorkForId
            };

            ViewData["id"] = id;
            ViewBag.Departments = _departmentRepository.GetAll();

            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id , EmployeeDto employeeDto )
        {
            if (ModelState.IsValid)
            {
              
                var existingEmployee = _employeeRepository.GetById(id);
                if (existingEmployee == null)
                {
                    return NotFound($"gggggggg   id {id}");
                }
                existingEmployee.Name = employeeDto.Name;
                existingEmployee.Age = employeeDto.Age;
                existingEmployee.Email = employeeDto.Email;
                existingEmployee.Phone = employeeDto.Phone;
                existingEmployee.Address = employeeDto.Address;
                existingEmployee.Salary = employeeDto.Salary;
                existingEmployee.HiringDate = employeeDto.HiringDate;
                existingEmployee.WorkFor= employeeDto.WorkFor;
                existingEmployee.WorkForId= employeeDto.WorkForId;

                _employeeRepository.Update(existingEmployee);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index),employeeDto);
        }



        [HttpGet] 
        public IActionResult DeleteForm(int id)
        {
            var employee = _employeeRepository.GetWithDepartment(id);
            if (employee == null)
            {
                return NotFound();
            }



            return View(employee);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            
            if (employee != null)
            {
                employee.WorkFor = null;
                employee.WorkForId = null;



                _employeeRepository.Delete(employee);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
