using System.ComponentModel.DataAnnotations;

namespace Rice.SDK.Domain.Contract
{
    public interface IIdentifiableEntity
    {
        [Key]
        int Id { get; set; }
    }
}
