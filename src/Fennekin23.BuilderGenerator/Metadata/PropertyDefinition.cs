namespace Fennekin23.BuilderGenerator.Metadata;

public sealed class PropertyDefinition(string name, TypeDefinition type) : IEquatable<PropertyDefinition>
{
    public string Name { get; } = name;
    public TypeDefinition Type { get; } = type;
    
    public bool Equals(PropertyDefinition other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal)
               && Type.Equals(other.Type);
    }

    public override bool Equals(object? obj)
    {
        return obj is PropertyDefinition other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ Type.GetHashCode();
        }
    }
}