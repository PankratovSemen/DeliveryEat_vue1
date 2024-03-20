using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using System.Security.Cryptography;
using DeliveryEat_vue1.Server.Model;
using USR = DeliveryEat_vue1.Server.DataBase.User;
using Microsoft.Extensions.Logging;

namespace DeliveryEat_vue1.Server.Controllers
{
    public class AccountController : Controller
    {
        private UserContext _userContext;
        private ILogger<Startup> _logger;
        
        public AccountController(UserContext userContext,ILogger<Startup> logger)
        {
            userContext = _userContext;
            logger = _logger;
            if (userContext.Users.ToList() == null)
            {
                //Create Roles for Users and save systems
                Roles roles = new Roles { Id =5,RoleTitle = "Admin" };
                userContext.Roles.Add(roles);
                userContext.SaveChanges();
                roles = new Roles { Id=1,RoleTitle = "Manager" };
                userContext.Roles.Add(roles);
                userContext.SaveChanges();
                roles = new Roles {Id=2, RoleTitle = "User" };
                userContext.Roles.Add(roles);
                userContext.SaveChanges();
                roles = new Roles { Id=3,RoleTitle = "Kitchen" };
                userContext.Roles.Add(roles);
                userContext.SaveChanges();
                roles = new Roles {Id=4, RoleTitle = "Сourier" };
                userContext.Roles.Add(roles);
                userContext.SaveChanges();
                DeliveryEat_vue1.Server.DataBase.User user = new User
                {
                    Id=1,
                    Login = "Admin",
                    Password = "admin"
                };
                RolesToUsers rolesToUsers = new RolesToUsers
                {
                    UserId=1,
                    RoleId = 5
                };
                userContext.Users.Add(user);
                userContext.RolesToUSR.Add(rolesToUsers);
                userContext.SaveChanges();
                //Manager create

                 user = new User
                {
                    Id = 2,
                    Login = "Manager",
                    Password = "manager"
                };
                rolesToUsers = new RolesToUsers
                {
                    UserId = 2,
                    RoleId = 1
                };
                userContext.Users.Add(user);
                userContext.RolesToUSR.Add(rolesToUsers);
                userContext.SaveChanges();

                //Kitchen create
                user = new User
                {
                    Id = 3,
                    Login = "KT",
                    Password = "kitchen"
                };
                rolesToUsers = new RolesToUsers
                {
                    UserId = 3,
                    RoleId = 3
                };
                userContext.Users.Add(user);
                userContext.RolesToUSR.Add(rolesToUsers);
                userContext.SaveChanges();

                //Courier create
                user = new User
                {
                    Id = 4,
                    Login = "Courier",
                    Password = "courier"
                };
                rolesToUsers = new RolesToUsers
                {
                    UserId = 4,
                    RoleId = 4
                };
                userContext.Users.Add(user);
                userContext.RolesToUSR.Add(rolesToUsers);
                userContext.SaveChanges();

                //User create
                user = new User
                {
                    Id = 5,
                    Login = "User",
                    Password = "user"
                };
                rolesToUsers = new RolesToUsers
                {
                    UserId = 5,
                    RoleId = 2
                };
                userContext.Users.Add(user);
                userContext.RolesToUSR.Add(rolesToUsers);
                userContext.SaveChanges();


            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public JsonResult Login(string userLogin,string password)
        {
            try
            { 
                var keySize = 2048;
                var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keySize);
                EncriptionRSA encryption = new EncriptionRSA();
                userLogin = encryption.Encrypt(userLogin, rsaCryptoServiceProvider.ExportParameters(false));
                password = encryption.Encrypt(password, rsaCryptoServiceProvider.ExportParameters(false));
                USR User = _userContext.Users.Where(s => s.Login == userLogin && s.Password == password).FirstOrDefault();
                int idUSR = User.Id;
                RolesToUsers rolesTo = _userContext.RolesToUSR.Where(s => s.UserId == idUSR).FirstOrDefault();
                Roles roles = _userContext.Roles.Where(s => s.Id == rolesTo.RoleId).FirstOrDefault();
                return Json(roles.RoleTitle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("Exception");
            }




           
        }
    }
}
