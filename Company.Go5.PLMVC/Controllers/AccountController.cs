using System.Threading.Tasks;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;
using Company.Go5.PLMVC.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Go5.PLMVC.Controllers
{
    public class AccountController  : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region SignUP


        [HttpGet]

        public IActionResult SignUp() 
        {
            return View();
        
        }
        
        [HttpPost]

        public async Task<IActionResult> SignUp(SignUpDto model) 
        {

            if (ModelState.IsValid)
            {
                var olduser =await userManager.FindByNameAsync(model.UserName);

                if (olduser is null)
                {
                    olduser = await userManager.FindByEmailAsync(model.Email);
                    if (olduser is null)
                    {


                        var user = new AppUser()
                        {

                            UserName = model.UserName,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            IsAgree = model.IsAgree,


                        };

                        var result = await userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {

                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);



                        }



                    }

                }

                ModelState.AddModelError("", "invalid sign up");



            }

            
            return View();
        
        }

        #endregion


        #region SignIn


        [HttpGet]

        public ActionResult SignIn()
        {


            return View();

        }

        [HttpPost]

        public async Task<IActionResult> SignIn(SignInDto model )
        {

            if(ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.Email);

                if(user is not null)
                {

                    var flag = await userManager.CheckPasswordAsync(user, model.Password);

                    if(flag)
                    {
                        //sign in 
                        
                        
                      var result = await  signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                       
                        
                        
                        if(result.Succeeded)
                        {


                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }



                    }

                }

                ModelState.AddModelError("", "Invalid Login ! ");
            }


            return View();

        }
        #endregion


        #region SignOut

        #endregion




        #region forget password


        [HttpGet]

        public IActionResult ForgetPassword()
        {

            return View();
        
        }

        [HttpPost]

        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model )
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    //generate token

                    var token = await userManager.GeneratePasswordResetTokenAsync(user);


                    //create url 
                   var url = Url.Action("ResetPassword", "Account", 
                        new {email=model.Email, token},Request.Scheme);




                    //create email 

                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "reset password",
                        Body = url


                    };


                    //send email

                   var flag= EmailSettings.SendEmail(email);

                    if(flag)
                    {

                        //check your inbox

                        return RedirectToAction("CheckYourInbox");


                    }



                }

            }

            ModelState.AddModelError("", "invalid reset password operation  ");




            return View("ForgetPassword",model);
        }



        [HttpGet]

        public IActionResult CheckYourInbox()
        {


            return View();
        }
        

        [HttpGet]

        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();

        }
        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordDto model )
        {
            if(ModelState.IsValid)
            {


                var email = TempData["email"] as string ;
                var token = TempData["token"] as string ;
                if(email is null || token is null)
                {
                    return BadRequest("invalid operations ");

                }


                var user = await userManager.FindByEmailAsync(email);
                if(user is not null)
                {

                    var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("SignIn");
                    }

                }

                ModelState.AddModelError("", "invalid ");
            }

            return View();

        }




        #endregion


    }



}
