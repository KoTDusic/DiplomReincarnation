using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronDecanat.Auth;
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
        public async Task<IActionResult> Register(RegistrationUser user, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                using (var db = new FirebirdDb())
                {
                    if (db.Accounts.Any(u => u.Username.Equals(user.Username)))
                    {
                        ModelState.AddModelError("user-exist-error",
                            $"Пользователь с именем {user.Username} уже зарегистрирован.");
                        return View();
                    }

                    user.Id = db.InsertWithInt32Identity(user);
                }

                await Authenticate(user.Username);

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [
        HttpPost]
        public async Task<IActionResult> Login(User user, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                using (var db = new FirebirdDb())
                {
                    if (!db.Accounts.Any(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)))
                    {
                        ModelState.AddModelError("user-login-error",
                            $"Пользователь с таким именем и паролем не найден");
                        return View();
                    }
                }

                await Authenticate(user.Username);

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

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, userName));
            if (userName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, UserType.Admin));
            }
            else if (userName == "Dekan")
            {
                claims.Add(new Claim(ClaimTypes.Role, UserType.Decanat));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, UserType.Teacher));
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
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