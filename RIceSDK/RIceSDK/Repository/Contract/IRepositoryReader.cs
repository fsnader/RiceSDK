using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RIceSDK.Domain.Contract;
using RIceSDK.Utils;

namespace RIceSDK.Repository.Contract
{
    public interface IRepositoryReader<T>
        where T : IIdentifiableEntity  
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters);
        Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression);
        IQueryable<T> GetIQueryable();
    }
}