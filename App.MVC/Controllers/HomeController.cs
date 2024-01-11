using Microsoft.AspNetCore.Mvc;

namespace App.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();  
        }
    }
}
