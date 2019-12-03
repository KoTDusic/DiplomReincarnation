using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronDecanat.Repozitory;
using FirebirdDatabaseProviders;
using LinqToDB;
using LinqToDB.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElectronDecanat.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationTeacher user, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                using (var db = new FirebirdDb())
                {
                    if (UnitOfWork.Teachers.GetAll().Any(u => u.Username.Equals(user.Username)))
                    {
                        ModelState.AddModelError(string.Empty,
                            $"Пользователь с именем {user.Username} уже зарегистрирован.");
                        return View();
                    }

                    user.Role = user.Username == "Admin" ? Teacher.AdminRole : Teacher.UndefinedRole;
                    user.Id = db.InsertWithInt32Identity(user);
                }

                await Authenticate(user);

                if (returnUrl.IsNullOrEmpty())
                {
                    return RedirectToAction("Index", "Main");
                }

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,
                    $"Не удалось зарегистрироваться.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [
        HttpPost]
        public async Task<IActionResult> Login(Teacher user, string returnUrl)
        {
            try
            {
                if (user.Username == null || user.Password == null)
                {
                    return View();
                }

                var loggedUser = UnitOfWork.Teachers.GetAll()
                    .First(u => u.Username.Equals(user.Username)
                                && u.Password.Equals(user.Password));
                if (loggedUser == null)
                {
                    ModelState.AddModelError(string.Empty,
                        $"Пользователь с таким именем и паролем не найден");
                    return View();
                }

                await Authenticate(loggedUser);

                if (returnUrl.IsNullOrEmpty())
                {
                    return RedirectToAction("Index", "Main");
                }

                return Redirect(returnUrl);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }

        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return RedirectToAction(nameof(Login), new {ReturnUrl = returnUrl});
        }

        private async Task Authenticate(Teacher user)
        {
            // создаем один claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            // создаем объект ClaimsIdentity
            var id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Main");
        }
    }
}