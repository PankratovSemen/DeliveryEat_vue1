using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;

namespace DeliveryEat_vue1.Server.Controllers
{
    public class AccountController : Controller
    {
        private UserContext _userContext;
        public AccountController(UserContext userContext)
        {
            userContext = _userContext;
            if (userContext.Users.ToList() == null)
            {
                Roles roles = new Roles { RoleTitle = "Admin" };
                roles = new Roles { RoleTitle = "Manager" };
                roles = new Roles { RoleTitle = "User" };
                roles = new Roles { RoleTitle = "Kitchen" };
                roles = new Roles { RoleTitle = "Сourier" };

                
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(UserLogin userLogin)
        {
            User user;
            Microsoft.AspNetCore.Identity.SignInResult result;
            if (ModelState.IsValid && (user = await userManager.FindByEmailAsync(userLogin.Login)) != null
                && (result = await signInManager.PasswordSignInAsync(user, userLogin.Password, false, false)).Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
