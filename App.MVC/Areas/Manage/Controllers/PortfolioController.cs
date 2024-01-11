using App.Business.Services.Interfaces;
using App.Business.ViewModels.PortfolioVMs;
using App.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace App.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IPortfolioService _service;
        private readonly ICategoryRepository _repCat;

        public PortfolioController(IWebHostEnvironment env, IPortfolioService service, ICategoryRepository repCat)
        {
            _env = env;
            _service = service;
            _repCat = repCat;
        }

        public async Task<IActionResult> Table()
        {
            ViewData["Portfolios"] = await _service.GetAllAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _repCat.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePortfolioVM Portfolio)
        {
            await _service.CreateAsync(Portfolio, _env.WebRootPath);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var oldPortfolio = await _service.GetByIdAsync(Id);

            UpdatePortfolioVM updatePortfolioVM = new()
            {
                Title = oldPortfolio.Title,
                CategoryId = oldPortfolio.CategoryId,
                Categories = await _repCat.GetAllAsync()
            };

            return View(updatePortfolioVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePortfolioVM Portfolio)
        {
            await _service.UpdateAsync(Portfolio, _env.WebRootPath);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var Portfolio = await _service.GetByIdAsync(Id);

            return View(Portfolio);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);

            return RedirectToAction(nameof(Table));
        }
    }
}
