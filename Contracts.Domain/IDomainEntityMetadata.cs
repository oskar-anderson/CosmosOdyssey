namespace Contracts.Domain;

public interface IDomainEntityMetadata
{
    string? CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string? ChangedBy { get; set; }
    DateTime ChangedAt { get; set; }
}