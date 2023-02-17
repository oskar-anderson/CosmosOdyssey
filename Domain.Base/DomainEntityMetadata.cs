using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace Domain.Base;

public abstract class DomainEntityMetadata : IDomainEntityMetadata, IDomainEntityId<Guid>
{
    public required Guid Id { get; set; }
    
    [MaxLength(255)]
    [JsonIgnore]
    public string? CreatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [MaxLength(255)]
    [JsonIgnore]
    public string? ChangedBy { get; set; }
    
    [JsonIgnore]
    public DateTime ChangedAt { get; set; }
}