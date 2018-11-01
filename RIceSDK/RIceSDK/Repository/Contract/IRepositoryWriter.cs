using System.Threading.Tasks;
using RIceSDK.Domain.Contract;

namespace RIceSDK.Repository.Contract
{
    public interface IRepositoryWriter<T>
        where T : IIdentifiableEntity
    {
        Task<int> Save(T entity);
        Task<int> Delete(T entity);
    }
}