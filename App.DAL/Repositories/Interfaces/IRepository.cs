using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository<T> where T : BaseAuditableEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(
            params string[] includes
            );
        Task<T> GetByIdAsync(int id, params string[] inlcudes);
        Task<T> CreateAsync(T entity); 
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
