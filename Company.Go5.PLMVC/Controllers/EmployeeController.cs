using AutoMapper;
using Company.Go5.BLL.Interfaces;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Company.Go5.PLMVC.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Company.Go5.BLL.Interfaces;
using Company.Go5.PLMVC.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Company.Go5.PLMVC.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository
            ,IMapper mapper ,IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string? SearchInput)
        {
           // var employees = _employeeRepository.GetAllWithDepartment();
            var employees = await unitOfWork.employeeRepository.GetAllWithDepartmentAsync();

            if (!string.IsNullOrEmpty(SearchInput))
            {

               // employees = _employeeRepository.GetAllByName(SearchInput);
                employees = await unitOfWork.employeeRepository.GetAllByNameAsync(SearchInput);
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) {

            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }
            if(id <= 0)
            {
                return BadRequest("correct id required");
            }




           // var employee = _employeeRepository.GetWithDepartment(id);
            var employee = await unitOfWork.employeeRepository.GetWithDepartmentAsync(id);



            return View(employee);
        }


        [HttpGet]

        public async Task<IActionResult> CreateForm()
        {


           // ViewBag.Departments = _departmentRepository.GetAll();
            ViewBag.Departments =await unitOfWork.departmentRepository.GetAllAsync();

            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee= new Employee();
               employee= mapper.Map<Employee>(employeeDto);


                if(employeeDto.Image is not null)
                {

                   employee.ImageName= AttachmentSettings.Upload(employeeDto.Image, "images");



                }




                //var employee = new Employee
                //{
                //    Name = employeeDto.Name,
                //    Age = employeeDto.Age,
                //    Email = employeeDto.Email,
                //    Phone = employeeDto.Phone,
                //    Address = employeeDto.Address,
                //    Salary = employeeDto.Salary,
                //    HiringDate = employeeDto.HiringDate,
                //    CreateAt = DateTime.Now,
                //    IsActive = true,
                //    IsDeleted = false,
                //    WorkForId = employeeDto.WorkForId
                //};
                //_employeeRepository.Add(employee);
                unitOfWork.employeeRepository.Add(employee);

                var count = await unitOfWork.CompleteAsync();
            }

            else { return BadRequest(ModelState); }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
            //var employee = _employeeRepository.GetById(id);
            var employee = await unitOfWork.employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto();

            employeeDto= mapper.Map<EmployeeDto>(employee);


            //var employeeDto = new EmployeeDto
            //{

            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Phone = employee.Phone,
            //    Address = employee.Address,
            //    Salary = employee.Salary,
            //    HiringDate = employee.HiringDate,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId
            //};

            ViewData["id"] = id;
           // ViewBag.Departments = _departmentRepository.GetAll();
            ViewBag.Departments =await unitOfWork.departmentRepository.GetAllAsync();

            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id , EmployeeDto employeeDto )
        {
            if (ModelState.IsValid)
            {
              
                if(employeeDto.ImageName is not null&& employeeDto.Image is not null)
                {

                    AttachmentSettings.Unload(employeeDto.ImageName, "images");

                   




                }


                if(employeeDto.Image is not null)
                {
                    employeeDto.ImageName = AttachmentSettings.Upload(employeeDto.Image, "images");
                }



                //var existingEmployee = _employeeRepository.GetById(id);
                var existingEmployee = await unitOfWork.employeeRepository.GetByIdAsync(id);
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
                existingEmployee.WorkFor = employeeDto.WorkFor;
                existingEmployee.WorkForId = employeeDto.WorkForId;



              //  _employeeRepository.Update(existingEmployee);
                unitOfWork.employeeRepository.Update(existingEmployee);
                var count = await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index),employeeDto);
        }



        [HttpGet] 
        public async Task<IActionResult> DeleteForm(int id)
        {
            //var employee = _employeeRepository.GetWithDepartment(id);
            var employee = await unitOfWork.employeeRepository.GetWithDepartmentAsync(id);
            if (employee == null)
            {
                return NotFound();
            }



            return View(employee);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Employee employee)
        {
            
            if (employee != null)
            {
                employee.WorkFor = null;
                employee.WorkForId = null;



                //  _employeeRepository.Delete(employee);
                unitOfWork.employeeRepository.Delete(employee);
                var count = await unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    AttachmentSettings.Unload(employee.ImageName, "images");

                }


                

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
