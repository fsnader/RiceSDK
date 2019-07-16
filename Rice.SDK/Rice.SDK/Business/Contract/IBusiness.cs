using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Rice.SDK.Utils;

namespace Rice.SDK.Business.Contract
{
    public interface IBusiness<T>
    {
        Task<IEnumerable<T>> ListAll(ClaimsPrincipal user = null);
        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression, ClaimsPrincipal user = null);

        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties);
        
        Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters, ClaimsPrincipal user = null);
        Task<T> GetById(int id, ClaimsPrincipal user = null);
        Task<int> Save(T entity, ClaimsPrincipal user = null);
        Task<int> Delete(T entity, ClaimsPrincipal user = null);
    }
}