using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Utils;

namespace Rice.SDK.Business.Contract
{
    public interface IDataTransferBusiness<TDataTransfer, TEntity>
        where TDataTransfer : IDataTransferEntity<TEntity>
        where TEntity : IIdentifiableEntity
    {
        Task<IEnumerable<TDataTransfer>> ListAll(ClaimsPrincipal user = null);
        Task<IEnumerable<TDataTransfer>> ListByExpression(Expression<Func<TEntity, bool>> expression, ClaimsPrincipal user = null);
        Task<IEnumerable<TDataTransfer>> ListByFilter(FilterParameters filterParameters, ClaimsPrincipal user = null);
        Task<TDataTransfer> GetById(int id, ClaimsPrincipal user = null);
        Task<int> Save(TDataTransfer entity, ClaimsPrincipal user = null);
        Task<int> Delete(TDataTransfer entity, ClaimsPrincipal user = null);
    }
}