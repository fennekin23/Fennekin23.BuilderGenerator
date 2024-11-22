namespace Fennekin23.BuilderGenerator.Metadata;

public sealed class TypeDefinition(string name, bool isNullable) : IEquatable<TypeDefinition>
{
    public string Name { get; } = name;

    public bool IsNullable { get; } = isNullable;
    
    public bool Equals(TypeDefinition? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.Ordinal)
               && IsNullable == other.IsNullable;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TypeDefinition other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ IsNullable.GetHashCode();
        }
    }
}