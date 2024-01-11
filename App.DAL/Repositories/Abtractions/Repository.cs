using App.Core.Entities.Common;
using App.DAL.Context;
using App.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Abtractions
{
    public class Repository<T> : ICategoryRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync(
            params string[] includes
            )
        {
            IQueryable<T> query = _table;
            if(includes is not null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.Where(x => !x.IsDeleted);
        }

        public Task<T> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x => x.Id == id);

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.Where(x => !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _db.AddAsync(entity);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            (await GetByIdAsync(id)).IsDeleted = true;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _db.SaveChangesAsync();

            return result;
        }
    }
}
