using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using System.Security.Cryptography;
using DeliveryEat_vue1.Server.Model;
using USR = DeliveryEat_vue1.Server.DataBase.User;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DeliveryEat_vue1.Server.Controllers
{
    public class AccountController : Controller
    {
       
        private ILogger<Startup> _logger;
        private ApplicationContext context;

        public AccountController( ILogger<Startup> _logger, ApplicationContext context)
        {
            this.context = context;
            this._logger = _logger;
            //if (userContext.Users.ToList() == null)
            //{
            //    //Create Roles for Users and save systems
            //    Roles roles = new Roles { Id =5,RoleTitle = "Admin" };
            //    userContext.Roles.Add(roles);
            //    userContext.SaveChanges();
            //    roles = new Roles { Id=1,RoleTitle = "Manager" };
            //    userContext.Roles.Add(roles);
            //    userContext.SaveChanges();
            //    roles = new Roles {Id=2, RoleTitle = "User" };
            //    userContext.Roles.Add(roles);
            //    userContext.SaveChanges();
            //    roles = new Roles { Id=3,RoleTitle = "Kitchen" };
            //    userContext.Roles.Add(roles);
            //    userContext.SaveChanges();
            //    roles = new Roles {Id=4, RoleTitle = "Сourier" };
            //    userContext.Roles.Add(roles);
            //    userContext.SaveChanges();
            //    DeliveryEat_vue1.Server.DataBase.User user = new User
            //    {
            //        Id = 1,
            //        Login = EncryptionHash.Encrypt("Admin"),
            //        Password = EncryptionHash.Encrypt("admin")
            //    };
            //    RolesToUsers rolesToUsers = new RolesToUsers
            //    {
            //        UserId=1,
            //        RoleId = 5
            //    };
            //    userContext.Users.Add(user);
            //    userContext.RolesToUSR.Add(rolesToUsers);
            //    userContext.SaveChanges();
            //    //Manager create

            //     user = new User
            //    {
            //        Id = 2,
            //        Login = EncryptionHash.Encrypt("Manager"),
            //        Password = EncryptionHash.Encrypt("manager")
            //    };
            //    rolesToUsers = new RolesToUsers
            //    {
            //        UserId = 2,
            //        RoleId = 1
            //    };
            //    userContext.Users.Add(user);
            //    userContext.RolesToUSR.Add(rolesToUsers);
            //    userContext.SaveChanges();

            //    //Kitchen create
            //    user = new User
            //    {
            //        Id = 3,
            //        Login = EncryptionHash.Encrypt("KT"),
            //        Password = EncryptionHash.Encrypt("kitchen")
            //    };
            //    rolesToUsers = new RolesToUsers
            //    {
            //        UserId = 3,
            //        RoleId = 3
            //    };
            //    userContext.Users.Add(user);
            //    userContext.RolesToUSR.Add(rolesToUsers);
            //    userContext.SaveChanges();

            //    //Courier create
            //    user = new User
            //    {
            //        Id = 4,
            //        Login = EncryptionHash.Encrypt("Courier"),
            //        Password = EncryptionHash.Encrypt("courier")
            //    };
            //    rolesToUsers = new RolesToUsers
            //    {
            //        UserId = 4,
            //        RoleId = 4
            //    };
            //    userContext.Users.Add(user);
            //    userContext.RolesToUSR.Add(rolesToUsers);
            //    userContext.SaveChanges();

            //    //User create
            //    user = new User
            //    {
            //        Id = 5,
            //        Login = EncryptionHash.Encrypt("User"),
            //        Password = EncryptionHash.Encrypt("user")
            //    };
            //    rolesToUsers = new RolesToUsers
            //    {
            //        UserId = 5,
            //        RoleId = 2
            //    };
            //    userContext.Users.Add(user);
            //    userContext.RolesToUSR.Add(rolesToUsers);
            //    userContext.SaveChanges();


        
    
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string login,string password)
        {
            try
            {
                var identity = GetIdentity(login, password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                        claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("Exception");
            }




           
        }
        [Route("Register")]
        [HttpPost]
        public JsonResult Register(string surname, string name, string middlename, string login, string password, string role,string Email)
        {
            var user = new User()
            {
                Name = name,
                Surname = surname,
                MiddleName = middlename,
                Email = Email,
                Login = EncryptionHash.Encrypt(login),
                Password = EncryptionHash.Encrypt(password)
            };
            context.Users.Add(user);
            context.SaveChanges();
            int id = user.Id;
            var roles  = context.Roles.Where(x => x.RoleTitle == role).FirstOrDefault();

            var RolesToUser = new RolesToUsers
            {
                RoleId = roles.Id,
                UserId = id
            };
            context.RolesToUSR.Add(RolesToUser);
            context.SaveChanges();

            return Json(context.Users.Where(x=>x.Id== id).FirstOrDefault());
        }


        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var user = context.Users.Where(x=>x.Login==EncryptionHash.Encrypt(login) && x.Password==EncryptionHash.Encrypt(password)).FirstOrDefault();
                if (user != null)
                {
                    int roleId = context.RolesToUSR.Where(x=>x.UserId==user.Id).FirstOrDefault().Id;
                    var roled = context.Roles.Where(x=>x.Id==roleId).FirstOrDefault();
                    var claims = new List<Claim>
                    {
                        new Claim("username", user.Login),
                        new Claim("role", roled.RoleTitle)
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                return null;

            }


        [Route("AddRole")]
        [HttpPost]
        public JsonResult AddRole(string role)
        {
            var roles = new Roles
            {
                RoleTitle = role
            };
            context.Roles.Add(roles);
            context.SaveChanges();
            return Json("Ok");
        }
    }
}
