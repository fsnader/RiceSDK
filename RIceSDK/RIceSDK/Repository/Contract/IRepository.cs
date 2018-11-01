using Microsoft.EntityFrameworkCore;
using RIceSDK.Domain.Contract;

namespace RIceSDK.Repository.Contract
{
    public interface IRepository<T, TContext> : IRepositoryReader<T>, IRepositoryWriter<T>
        where T : class, IIdentifiableEntity
        where TContext : DbContext
    {
        
    }
}