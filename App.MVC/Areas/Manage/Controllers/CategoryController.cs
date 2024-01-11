using App.Business.Services.Interfaces;
using App.Business.ViewModels.CategoryVMs;
using Microsoft.AspNetCore.Mvc;

namespace App.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Table()
        {
            ViewData["Categories"] = await _service.GetAllAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM category)
        {
            await _service.CreateAsync(category);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var oldCategory = await _service.GetByIdAsync(Id);

            UpdateCategoryVM updateCategoryVM = new()
            {
                Name = oldCategory.Name
            };

            return View(updateCategoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryVM category)
        {
            await _service.UpdateAsync(category);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var category = await _service.GetByIdAsync(Id);

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);

            return RedirectToAction(nameof(Table));
        }
    }
}
