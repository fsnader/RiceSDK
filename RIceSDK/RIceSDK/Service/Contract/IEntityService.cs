using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RIceSDK.Domain.Contract;
using RIceSDK.Utils;

namespace RIceSDK.Service.Contract
{
    public interface IEntityService<T>
        where T : IIdentifiableEntity
    {
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters);
        Task<T> GetById(int id);
        Task<int> Save(T entity);
        Task<int> Delete(T entity);
    }
}