using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        //ask clr to create object of DepartmentRepository
        //when assigning it reference of type of IDepartmentRepository
        //by making dependency injection in services region 
        public DepartmentController(IDepartmentRepository departmentRepository) 
        { 
        
            _departmentRepository = departmentRepository;
        }

        [HttpGet]  //get : / Department/Index 
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Create(DepartmentDto department)
        {
            if(ModelState.IsValid) //server side validation 
            {
                //manual mapping 
                var NewDepartment = new Department
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreateAt = department.CreateAt
                };
                _departmentRepository.Add(NewDepartment);

            }

            

            






            return RedirectToAction(nameof(Index) );

        }
    }
}

