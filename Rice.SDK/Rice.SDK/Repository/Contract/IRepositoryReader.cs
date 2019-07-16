using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Utils;
using Rice.SDK.Domain;

namespace Rice.SDK.Repository.Contract
{
    public interface IRepositoryReader<T>
        where T : IIdentifiableEntity  
    {
        Task<T> GetById(int id);
        Task<T> GetById(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> ListAll(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters);
        Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters, 
            params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression, 
            params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetIQueryable();
    }
}