namespace Fennekin23.BuilderGenerator.Metadata;

public class PropertyDefinition(string name, string type) : IEquatable<PropertyDefinition>
{
    public string Name { get; } = name;
    public string Type { get; } = type;
    
    public bool Equals(PropertyDefinition other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal)
               && string.Equals(Type, other.Type, StringComparison.Ordinal);
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