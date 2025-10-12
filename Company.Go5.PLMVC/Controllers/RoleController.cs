using AutoMapper;
using Company.Go5.BLL;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Company.Go5.PLMVC.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
           
            _roleManager = roleManager;
        }



        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturn> roles;


            if (string.IsNullOrEmpty(SearchInput))
            {

                roles = _roleManager.Roles.Select(u => new RoleToReturn()
                {
                    Id = u.Id,
                    Name = u.Name

                });


            }

            else
            {

                roles = _roleManager.Roles.Select(u => new RoleToReturn()
                {
                    Id = u.Id,
                    Name = u.Name

                }).Where(r => r.Name.ToLower().Contains(SearchInput.ToLower()));





            }

            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            if (id is null)
            {
                return BadRequest("correct id required");
            }




            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) { return NotFound(new { StatusCode = 404 }); }

            var roledto = new RoleToReturn()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roledto);
        }






        [HttpGet]

        public async Task<IActionResult> CreateForm()
        {



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleToReturn roledto)
        {
            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByNameAsync(roledto.Name);

                if(role is null)
                {

                    role = new IdentityRole()
                    {

                        Name=roledto.Name


                    };


                   var result = await _roleManager.CreateAsync(role);
                        
                    if(result.Succeeded)
                    {

                        return RedirectToAction("Index");

                    }

                    

                }


            }

            return BadRequest();
                
        }















        [HttpGet]
        public async Task<IActionResult> EditForm(string? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roledto = new RoleToReturn()
            {
                Id = role.Id,
                Name = role.Name
            };



            return View(roledto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleToReturn RoleDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }




            if (id == null || id != RoleDto.Id) { return BadRequest(); }



            var existingRole = await _roleManager.FindByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound($" no user has  id = {id}");
            }


            existingRole.Name=RoleDto.Name;


            var result = await _roleManager.UpdateAsync(existingRole);


            if (!result.Succeeded)
            { return BadRequest(); }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteForm(string? id)
        {
            if (id is null) { return BadRequest(); }

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) { return NotFound(); }

            var roledto = new RoleToReturn()
            {

                Id = role.Id,
                Name = role.Name
            };


            return View(roledto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleToReturn model)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            if (model is null) { return BadRequest(); }

            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null) { return NotFound(); }
            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest();

            }


            return RedirectToAction(nameof(Index));
        }


    }
}
