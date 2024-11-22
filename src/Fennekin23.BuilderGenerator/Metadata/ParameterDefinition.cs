namespace Fennekin23.BuilderGenerator.Metadata;

public sealed class ParameterDefinition(string name, TypeDefinition type) : IEquatable<ParameterDefinition>
{
    public string Name { get; } = name;
    public TypeDefinition Type { get; } = type;

    public bool Equals(ParameterDefinition other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal) 
               && Type.Equals(other.Type);
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