using App.Business.Exceptions.Portfolio;
using App.Business.Helpers;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.PortfolioVMs;
using App.Core.Entities;
using App.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Impelemtations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _rep;
        private string[] includes = { "Category" };

        public PortfolioService(IPortfolioRepository rep)
        {
            _rep = rep;
        }

        public async Task CreateAsync(CreatePortfolioVM entity, string env)
        {
            if (entity.File.CheckLength(3000000)) throw new PortfolioArgumentException("Image must be lower than 2MB!", nameof(entity.File));
            if (entity.File.CheckType("image/")) throw new PortfolioArgumentException("File must be image format!", nameof(entity.File));

            Portfolio newPortfolio = new()
            {
                Title = entity.Title,
                CoverImageUrl = entity.File.Upload(env, @"\Upload\PortfolioImages\"),
                CategoryId = entity.CategoryId
            };

            await _rep.CreateAsync(newPortfolio);
            await _rep.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _rep.DeleteAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task<IQueryable<Portfolio>> GetAllAsync()
        {
            IQueryable<Portfolio> query = await _rep.GetAllAsync(includes: "Category");

            return query;
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            return await _rep.GetByIdAsync(id, includes);
        }

        public async Task UpdateAsync(UpdatePortfolioVM entity, string env)
        {
            Portfolio oldPortfolio = await _rep.GetByIdAsync(entity.Id);
            oldPortfolio.Title = entity.Title;
            oldPortfolio.CategoryId = entity.CategoryId;

            if(entity.File is not null)
            {
                FileManager.Delete(oldPortfolio.CoverImageUrl, env, @"\Upload\PortfolioImages\");
                oldPortfolio.CoverImageUrl = entity.File.Upload(env, @"\Upload\PortfolioImages\");
            }

            await _rep.UpdateAsync(oldPortfolio);
            await _rep.SaveChangesAsync();
        }
    }
}
