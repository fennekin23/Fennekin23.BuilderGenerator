namespace Fennekin23.BuilderGenerator.Metadata;

public class ParameterDefinition(string name, string type) : IEquatable<ParameterDefinition>
{
    public string Name { get; } = name;
    public string Type { get; } = type;

    public bool Equals(ParameterDefinition other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal) 
               && string.Equals(Type, other.Type, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return obj is ParameterDefinition other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ Type.GetHashCode();
        }
    }
}