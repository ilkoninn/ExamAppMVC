using App.Business.ViewModels.PortfolioVMs;
using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<IQueryable<Portfolio>> GetAllAsync();
        Task<Portfolio> GetByIdAsync(int id);
        Task CreateAsync(CreatePortfolioVM entity, string env);
        Task UpdateAsync(UpdatePortfolioVM entity, string env);
        Task DeleteAsync(int id);
    }
}
