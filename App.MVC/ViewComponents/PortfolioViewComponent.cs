using App.Business.Services.Interfaces;
using App.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace App.MVC.ViewComponents
{
    public class PortfolioViewComponent : ViewComponent
    {
        private readonly IPortfolioService _service;

        public PortfolioViewComponent(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IQueryable<Portfolio> query = await _service.GetAllAsync();

            return View(query);
        }
    }
}
