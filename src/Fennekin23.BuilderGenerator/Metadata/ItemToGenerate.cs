namespace Fennekin23.BuilderGenerator.Metadata;

public class ItemToGenerate(
    string name,
    string nameSpace,
    string accessibility,
    IEnumerable<ParameterDefinition> constructorParameters,
    IEnumerable<PropertyDefinition> properties) : IEquatable<ItemToGenerate>
{
    public string Name { get; } = name;
    public string NameSpace { get; } = nameSpace;
    public string Accessibility { get; } = accessibility;
    public string FullyQualifiedName { get; } = $"{nameSpace}.{name}";
    public IEnumerable<ParameterDefinition> ConstructorParameters { get; } = constructorParameters;
    public IEnumerable<PropertyDefinition> Properties { get; } = properties;

    public bool Equals(ItemToGenerate other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal)
               && string.Equals(NameSpace, other.NameSpace, StringComparison.Ordinal)
               && string.Equals(Accessibility, other.Accessibility, StringComparison.Ordinal)
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
            hashCode = (hashCode * 397) ^ NameSpace.GetHashCode();
            hashCode = (hashCode * 397) ^ Accessibility.GetHashCode();
            hashCode = (hashCode * 397) ^ FullyQualifiedName.GetHashCode();
            hashCode = (hashCode * 397) ^ ConstructorParameters.GetHashCode();
            hashCode = (hashCode * 397) ^ Properties.GetHashCode();
            return hashCode;
        }
    }
}