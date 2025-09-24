using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;

        //ask clr to create object of DepartmentRepository
        //when assigning it reference of type of IDepartmentRepository
        //by making dependency injection in services region 
        public DepartmentController(IDepartmentRepository repository) 
        { 
        
            _repository = repository;
        }

        [HttpGet]  //get : / Department/Index 
        public IActionResult Index()
        {
            var departments = _repository.GetAll();

            return View(departments);
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
