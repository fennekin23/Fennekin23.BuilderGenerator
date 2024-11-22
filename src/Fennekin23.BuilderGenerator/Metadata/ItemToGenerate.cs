namespace Fennekin23.BuilderGenerator.Metadata;

public sealed class ItemToGenerate(
    string name,
    string @namespace,
    string accessModifier,
    IEnumerable<ParameterDefinition> constructorParameters,
    IEnumerable<PropertyDefinition> properties) : IEquatable<ItemToGenerate>
{
    public string Name { get; } = name;
    public string Namespace { get; } = @namespace;
    public string AccessModifier { get; } = accessModifier;
    public string FullyQualifiedName { get; } = $"{@namespace}.{name}";
    public IEnumerable<ParameterDefinition> ConstructorParameters { get; } = constructorParameters;
    public IEnumerable<PropertyDefinition> Properties { get; } = properties;

    public bool Equals(ItemToGenerate other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal)
               && string.Equals(Namespace, other.Namespace, StringComparison.Ordinal)
               && string.Equals(AccessModifier, other.AccessModifier, StringComparison.Ordinal)
               && string.Equals(FullyQualifiedName, other.FullyQualifiedName, StringComparison.Ordinal)
               && ConstructorParameters.SequenceEqual(other.ConstructorParameters)
               && Properties.SequenceEqual(other.Properties);
    }

    public override bool Equals(object? obj)
    {
        return obj is ItemToGenerate other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ Namespace.GetHashCode();
            hashCode = (hashCode * 397) ^ AccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ FullyQualifiedName.GetHashCode();
            hashCode = (hashCode * 397) ^ ConstructorParameters.GetHashCode();
            hashCode = (hashCode * 397) ^ Properties.GetHashCode();
            return hashCode;
        }
    }
}