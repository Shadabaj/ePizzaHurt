using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaHurt.Models;
using PizzaHurt.Services.Interface;
using PizzaHurt.UI.Helpers;
using PizzaHurt.UI.Models;
using PizzaHut.Core.Entities;
using System.Security.Claims;
using System.Text.Json;

namespace PizzaHurt.UI.Controllers
{
    public class AccountController : Controller
    {

        IAuthService _authService;

        public AccountController(IAuthService authService) 
        { 
             _authService = authService;
        }

        [ServiceFilter(typeof(ActivityLogFilter))]
        public IActionResult Login()
        {
            return View();
        }

       
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginViewModel model,string returnUrl)
        {
            var user = _authService.ValidateUser(model.Email, model.Password);
            var emailExists = _authService.emailExists(model.Email);

            if (!emailExists) 
            {
                ModelState.AddModelError("Email", "Email does not exist.");
            }
            else
            {
                // If the user is null, the password is incorrect (since we verified email already)
                if (user == null)
                {
                    ModelState.AddModelError("Password", "Incorrect password.");
                }
                else
                {
                    Generateticeket(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                  
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }
            }
            return View(model);
        }

            [NonAction]
            private void Generateticeket(UserModel user)
            {
            string strdata = JsonSerializer.Serialize(user);
            var claims = new List<Claim> 
                { 
                new Claim(ClaimTypes.UserData, strdata),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,string.Join(",",user.Roles)),
                // Adding these for the  Activity Log Filter
                new Claim(ClaimTypes.Name, user.Name)
                };

            var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme); //claim object krta hain 
           // var principal = new ClaimsPrincipal(identity);
            
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            });
            }

        [ServiceFilter(typeof(ActivityLogFilter))]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login","Account");
        }


        public IActionResult UnAuthorized()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            var genderEntities = _authService.GetGenders().ToList();
            ViewBag.Gender = new SelectList(genderEntities, "GenderId", "GenderName");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Signin(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                // //UserModel data = authService.ValidateUser(model.Email, model.Password);
                bool exists = _authService.emailExists(model.Email);
                if (!exists)
                {
                    User user = new User 
                    {
                        Email= model.Email,
                        Name=model.Name,
                        Password=model.Password,
                        PhoneNumber=model.PhoneNumber,
                        CreatedDate=DateTime.Now,
                        Gender= model.Gender,
                        IsTermChecked=model.AcceptTerms
                        
                    };
                    bool result = _authService.CreateUser(user, "User");
                    if (result) 
                    {
                        return RedirectToAction(nameof(Login));
                    }   
                }
                else if(exists){
                    ModelState.AddModelError("Email","Email Already Exists Try Another Email");
                }
            }
            var genderEntities = _authService.GetGenders().ToList();
            ViewBag.Gender = new SelectList(genderEntities, "GenderId", "GenderName");
            return View();
        }

        [ServiceFilter(typeof(ActivityLogFilter))]
        public async Task GoogleLogin()
        {
          await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                 new AuthenticationProperties
                 {
                   RedirectUri = Url.Action("GoogleResponse")
                 });
        }

        [ServiceFilter(typeof(ActivityLogFilter))]
        public async Task<IActionResult> GoogleResponse()
        {
            // var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = authenticateResult.Principal?.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var issuer = claims?.FirstOrDefault()?.Issuer;
            var phoneNumber = claims?.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value ?? "00000000";
            var genderValue = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Gender)?.Value;
            int gender = genderValue == "male" ? 1 :
                         genderValue == "female" ? 2 :
                         genderValue == "transgender" ? 3 : 4;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Login));
            }

            var user = _authService.GoogleCheckIn(email);
            if (user != null)
            {
                Generateticeket(user);
                return RedirectToAction("Index", "Home", new { area = "User" });
            }

            var newUser = new User
            {
                Name = name,
                Email = email,
                CreatedDate = DateTime.Now,
                Issurer = issuer,
                PhoneNumber = phoneNumber,
                Gender = gender,
                EmailConfirmed = true, 
                IsTermChecked = false, 
                Password = "T7$fX!2bM#kV9@zQw3^nRpL%jG8&dA"
            };
            var creationResult =  _authService.CreateUser(newUser, "User");

            if (creationResult)
            {
                UserModel userauth = new UserModel
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    Roles =  ["User"]
                };
                Generateticeket(userauth);
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            //Console.WriteLine("User creation failed for email: " + email);
            return RedirectToAction("Error", "Home");
        }

    }
}
