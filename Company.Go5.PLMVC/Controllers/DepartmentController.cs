using Company.Go5.BLL;
using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork unitOfWork;

        //ask clr to create object of DepartmentRepository
        //when assigning it reference of type of IDepartmentRepository
        //by making dependency injection in services region 
        public DepartmentController(IDepartmentRepository departmentRepository,IUnitOfWork unitOfWork) 
        { 
        
            _departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]  //get : / Department/Index 
        public IActionResult Index()
        {

            //view storage : 
            //built in Dictionary<string,object or dynamic >
            //accessed by 3 properties inherited from Controller  :
            //1.ViewData: transfer extra information from controller(action) to view 
            //value type is object and access using ["key"]  :ViewData["Title"] = "Index";

            //2.ViewBag : transfer extra information from controller(action) to view 
            //value type is dynamic and access using .  : var x = ViewBag.Message;



            //3.TempData : transfer information from action to action in same request 

            ViewData["Message"] = "dictionary view data";


           // var departments = _departmentRepository.GetAll();
            var departments = unitOfWork.departmentRepository.GetAll();
            
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
            var count = 0;
            if (ModelState.IsValid) //server side validation 
            {
                //manual mapping 
                var NewDepartment = new Department
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreateAt = department.CreateAt
                };
                //_departmentRepository.Add(NewDepartment);
                unitOfWork.departmentRepository.Add(NewDepartment);
                 count = unitOfWork.Complete();
            }

            TempData["CreateStatu"]= count;

            return RedirectToAction(nameof(Index) );

        }



        [HttpGet]

        public IActionResult Details(int?id)
        {

            if(id is null) { return BadRequest("id required "); }

            var department = unitOfWork.departmentRepository.GetById(id.Value);
            if(department is null) { return NotFound($"no department with this id {id}") ; }


            var DepartmentDto = new DepartmentDto()
            {

                Code=department.Code,
                Name=department.Name,
                CreateAt = department.CreateAt


            };

            return View(DepartmentDto);
        }







        [HttpGet]

        public IActionResult Delete(int? id)
        {

            if (id is null) { return BadRequest("id required "); }

            var department = unitOfWork.departmentRepository.GetById(id.Value);
            if (department is null) { return NotFound($"no department with this id {id}"); }


            var DepartmentDto = new DepartmentDto()
            {

                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt


            };

            return View(DepartmentDto);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete( [FromRoute] int id , DepartmentDto DepartmentDto)
        {
           


            if (!ModelState.IsValid)
            {
                return View(DepartmentDto);
            }
           

            var department = unitOfWork.departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }


            unitOfWork.departmentRepository.Delete(department);
            var count = unitOfWork.Complete();
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));

            }


            return NotFound();


        }




        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id is null) { return BadRequest("id is required"); }

            var department = unitOfWork.departmentRepository.GetById(id.Value);

            if (department is null) { return NotFound($"no department with id {id}"); }

            var DepartmentDto = new DepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt


            };

            return View(DepartmentDto);



        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( DepartmentDto DepartmentDto ,[FromRoute] int id)
        {



            if (!ModelState.IsValid)
            {
                return View(DepartmentDto); 
            }


            var department = unitOfWork.departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            department.Name= DepartmentDto.Name;
            department.Code = DepartmentDto.Code;
            department.CreateAt= DepartmentDto.CreateAt;



             unitOfWork.departmentRepository.Update(department);
            var count = unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
         

            return View(DepartmentDto);


        }






        //[HttpGet]

        //public IActionResult Edit(int? id)
        //{
        //    if (id is null) { return BadRequest("id is required"); }

        //    var department = _departmentRepository.GetById(id.Value);

        //    if (department is null) { return NotFound($"no department with id {id}"); }



        //    return View(department);



        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, Department department)
        //{



        //    if (ModelState.IsValid)
        //    {


        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));

        //        }
        //    }

        //    return View(department);

        //}
    }
}

