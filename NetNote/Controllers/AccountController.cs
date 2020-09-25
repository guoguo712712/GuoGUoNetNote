using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using guoguo.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetNote.Models;
using NetNote.Models.Account;

namespace NetNote.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _Logger;
        private readonly UserManager<NoteUser> _UserManager;
        private readonly SignInManager<NoteUser> _SignInManager;

        public AccountController(UserManager<NoteUser> userManager,SignInManager<NoteUser> signInManager,
            ILogger<AccountController> logger)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Logger = logger;

        }

        public IActionResult Login(string returnurl=null)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnurl=null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                _Logger.LogInformation($"Logged in {model.UserName}");
                return RedirectToAction("Index", "Note");
            }
            else
            {
                _Logger.LogWarning($"Failed to log in {model.UserName}");
                ModelState.AddModelError("", "用户名或密码错误！");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new NoteUser { UserName=model.UserName,Email=model.Email};
            var result = await _UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _Logger.LogInformation($"User {model.UserName} was created.");
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;
            await _SignInManager.SignOutAsync();
            _Logger.LogInformation($"{userName} logger out.");
            return RedirectToAction("Index", "Note");
        }

    }
}