using System.ComponentModel.DataAnnotations;

namespace RIceSDK.Domain.Contract
{
    public interface IIdentifiableEntity
    {
        [Key]
        int Id { get; set; }
    }
}
