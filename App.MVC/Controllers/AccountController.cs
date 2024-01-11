using App.Business.Exceptions.Account;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Mvc;

namespace App.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                await _service.Register(register);

                return RedirectToAction(nameof(Login));   
            }
            catch (AccountArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            try
            {
                await _service.Login(login);

                return RedirectToAction("Index", "Home");
            }
            catch (AccountArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRoles();

            return RedirectToAction("Index", "Home");
        }
    }
}
