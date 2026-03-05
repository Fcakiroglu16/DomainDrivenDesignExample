namespace DomainDrivenDesignExample.API.SharedKernels;

public abstract class BaseEntity<TKey> : EntityBase
    where TKey : notnull
{
    public TKey Id { get; set; } = default!;

    protected override object?[] GetKeys()
    {
        return [Id];
    }
}

public abstract class Entity : EntityBase
{
    protected abstract override object?[] GetKeys();
}

public abstract class EntityBase
{
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        return GetKeys().SequenceEqual(((EntityBase)obj).GetKeys());
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var key in GetKeys())
            hash.Add(key);
        return hash.ToHashCode();
    }

    public static bool operator ==(EntityBase? left, EntityBase? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(EntityBase? left, EntityBase? right)
    {
        return !(left == right);
    }

    protected abstract object?[] GetKeys();
}