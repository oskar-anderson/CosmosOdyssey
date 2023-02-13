using Contracts.Domain;

namespace Domain.Base;

public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; } = default!;
}