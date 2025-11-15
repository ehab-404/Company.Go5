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
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? SearchInput)
        {
            var users =  await _userManager.Users.ToListAsync();

            if (!string.IsNullOrEmpty(SearchInput))
            {


                users = users.Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower())).ToList();


            }

            var result = new List<UserToReturnDto>();

            foreach(var u in users)
            {

                var roles = await _userManager.GetRolesAsync(u);
                result.Add(new UserToReturnDto()
                {

                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = roles.ToList()


                });

            }
                



            

            return View(result);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            if ( id is null )
            {
                return BadRequest("correct id required");
            }




           var user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(new { StatusCode = 404 }); }

            var userdto = new UserToReturnDto()
            {
                Id=user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result

            };

            return View(userdto);
        }





        [HttpGet]

        public IActionResult Create()
        {

            return View();

        }



        [HttpPost]

        public async Task<IActionResult> Create(UserToReturnDto userDto)
        {
            if (userDto is null) { return BadRequest("user dto is null "); }

            var user = new AppUser()
            {


                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,




            };

            await _userManager.CreateAsync(user);

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));

        }









        [HttpGet]
        public async Task<IActionResult> EditForm(string? id)
        {
            if (id is null) 
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.id = id;

            var UserDto = new UserToReturnDto()
            {

                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result



            };

            
            return View(UserDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserToReturnDto UserDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }




            if (id == null || id != UserDto.Id) { return BadRequest(); }



            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($" no user has  id = {id}");
            }


            existingUser.UserName = UserDto.UserName;
            existingUser.FirstName = UserDto.FirstName;
            existingUser.LastName = UserDto.LastName;
            existingUser.Email = UserDto.Email;
            var result = await _userManager.UpdateAsync(existingUser);


            if (!result.Succeeded)
            { return BadRequest(); }


            return RedirectToAction(nameof(Index));
        }

            [HttpGet]
        public async Task<IActionResult> DeleteForm(string? id)
        {
                if(id is null) { return BadRequest(); }

                var user = await _userManager.FindByIdAsync(id);

                if (user is null) { return NotFound(); }
          
            var userdto = new UserToReturnDto()
            {

                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result


            };


            return View(userdto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( UserToReturnDto model )
        {
            if (!ModelState.IsValid) {  return BadRequest(); }

            if(model is null) { return BadRequest(); }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null) {  return NotFound(); }
           var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
            {
                return BadRequest();

            }

            
            return RedirectToAction(nameof(Index));
        }


    }



}
