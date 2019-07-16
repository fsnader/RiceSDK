using Microsoft.EntityFrameworkCore;
using Rice.SDK.Domain.Contract;

namespace Rice.SDK.Repository.Contract
{
    public interface IRepository<T, TContext> : IRepositoryReader<T>, IRepositoryWriter<T>
        where T : class, IIdentifiableEntity
        where TContext : DbContext
    {
        
    }
}