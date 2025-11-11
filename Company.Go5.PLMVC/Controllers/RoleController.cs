using System.Threading.Tasks;
using AutoMapper;
using Company.Go5.BLL;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Company.Go5.PLMVC.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Go5.PLMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager ,UserManager<AppUser> userManager)
        {
           
            _roleManager = roleManager;
            _userManager = userManager;
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



        [HttpGet]

        public async Task<IActionResult> AddOrRemoveUser(string RoleId)
        {

            var role =await _roleManager.FindByIdAsync(RoleId);

            if(role is null) { return NotFound(); }

            ViewData["RoleId"] = role.Id;

            var users_in_role = new List<UserInRoleDto>();

            var allusers = await _userManager.Users.ToListAsync();


            foreach (var user in allusers)
            {
                var userInRole = new UserInRoleDto()
                {

                    UserId = user.Id,
                    UserName = user.UserName

                };


                var IsIn = await _userManager.IsInRoleAsync(user, role.Name);
                if (IsIn) { userInRole.IsSelected = true; }
                else { userInRole.IsSelected = false; }

                users_in_role.Add(userInRole);

            }

            return View(users_in_role);

        }


        [HttpPost]

        public async Task<IActionResult> AddOrRemoveUser(IEnumerable<UserInRoleDto> inRoleDtos , string RoleId)

        {


            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role is null) { return NotFound(); }


            if (ModelState.IsValid)
            {
                foreach(var user in inRoleDtos)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);

                    if(appuser is not null)
                    {
                        var IsIn = await _userManager.IsInRoleAsync(appuser, role.Name);
                        if (user.IsSelected && !IsIn)
                        {




                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }


                        else if (IsIn && !user.IsSelected)
                        {



                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                        }


                    }

                }







                return RedirectToAction(nameof(Edit), new {id=role.Id});



            }
            return View(inRoleDtos);

        }

    }


}
