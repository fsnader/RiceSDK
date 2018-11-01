using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIceSDK.Domain.Contract;
using RIceSDK.Repository.Contract;
using RIceSDK.Service.Contract;
using RIceSDK.Utils;

namespace RIceSDK.Service.Concrete
{
    public class BaseEntityService<T, TContext> : IEntityService<T>
        where T : class, IIdentifiableEntity
        where TContext : DbContext
    {
        private readonly IRepository<T, TContext> _repository;

        public BaseEntityService(IRepository<T, TContext> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> ListAll()
        {
            return await _repository.ListAll();
        }

        public async Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression)
        {
            return await _repository
                .ListByExpression(expression);
        }

        public async Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters)
        {
            return await _repository
                .ListByFilter(filterParameters);
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<int> Save(T entity)
        {
            return await _repository.Save(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await _repository.Delete(entity);
        }
    }
}
