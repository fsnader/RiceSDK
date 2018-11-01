using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIceSDK.Domain.Contract;
using RIceSDK.Repository.Contract;
using RIceSDK.Utils;

namespace RIceSDK.Repository.Concrete
{
    public class BaseRepository<T, TContext> : IRepository<T, TContext>
        where T : class, IIdentifiableEntity
        where TContext : DbContext

    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> ListAll()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters)
        {
            return await _context.Set<T>()
                .AsQueryable()
                .FilterBy(filterParameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return  _context.Set<T>()
                .AsQueryable();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> Save(T entity)
        {
            if (entity.Id == 0)
                _context.Set<T>().Add(entity);
            else
                _context.Set<T>().Update(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _context.Set<T>()
                .Remove(entity);

            return await _context.SaveChangesAsync();
        }
    }
}
