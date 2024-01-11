using App.Business.Services.Interfaces;
using App.Business.ViewModels.CategoryVMs;
using App.Core.Entities;
using App.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Impelemtations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _rep;

        public CategoryService(ICategoryRepository rep)
        {
            _rep = rep;
        }

        public async Task CreateAsync(CreateCategoryVM entity)
        {
            Category newCategory = new()
            {
                Name = entity.Name,
            };

            await _rep.CreateAsync(newCategory);
            await _rep.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _rep.DeleteAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task<IQueryable<Category>> GetAllAsync()
        {
            IQueryable<Category> query = await _rep.GetAllAsync();

            return query;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _rep.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateCategoryVM entity)
        {
            Category oldCategory = await _rep.GetByIdAsync(entity.Id);
            oldCategory.Name = entity.Name;

            await _rep.UpdateAsync(oldCategory);
            await _rep.SaveChangesAsync();
        }
    }
}
