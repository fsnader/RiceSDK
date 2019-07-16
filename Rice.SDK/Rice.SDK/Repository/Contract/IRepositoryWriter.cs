using System.Threading.Tasks;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Domain;

namespace Rice.SDK.Repository.Contract
{
    public interface IRepositoryWriter<T>
        where T : IIdentifiableEntity
    {
        Task<int> Save(T entity);
        Task<int> Delete(T entity);
    }
}