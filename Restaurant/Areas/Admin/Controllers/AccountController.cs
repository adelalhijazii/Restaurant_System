using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Areas.Admin.ViewModels;
using Restaurant.Models;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> signInManager)
        {
            UserManager = _userManager;
            SignInManager = signInManager;
        }

        // GET: AccountController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: AccountController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Enter Email And Password");
                    return View();
                }
                var User = new AppUser
                {
                    Email = collection.Email,
                    UserName = collection.Email
                };
                var Resault = await UserManager.CreateAsync(User, collection.Password);
                if (Resault.Succeeded)
                {
                    await SignInManager.SignInAsync(User, isPersistent: false);
                    return RedirectToAction("Index", "MasterCategoryMenu");

                }
                else
                {
                    ModelState.AddModelError("", "Wrong Email or Password");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Login
        public ActionResult Login(int id)
        {
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(int id, Login collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Email or Password not correct..!");
                    return View();
                }

                var Resualt = await SignInManager.PasswordSignInAsync(collection.Email, collection.Password, collection.RememberMe, false);
                if (Resualt.Succeeded)
                {
                    return RedirectToAction("Index", "MasterCategoryMenu");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password not correct..!");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
