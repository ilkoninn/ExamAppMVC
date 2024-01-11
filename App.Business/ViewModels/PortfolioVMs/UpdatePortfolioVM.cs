using App.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.ViewModels.PortfolioVMs
{
    public class UpdatePortfolioVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? File { get; set; }
        public int CategoryId { get; set; }
        public IQueryable<Category> Categories { get; set; }
    }
}
