using App.Business.ViewModels.CategoryVMs;
using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryVM entity);
        Task UpdateAsync(UpdateCategoryVM entity);
        Task DeleteAsync(int id);
    }
}
