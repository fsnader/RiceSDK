namespace Rice.SDK.Domain.Contract
{
    public interface IDataTransferEntity<T> 
        where T : IIdentifiableEntity
    {
        int Id { get; set; }
        T Create();
        T Update(T entity);
    }
}