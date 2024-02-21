using System.Collections.Generic;

namespace Fennekin23.BuilderGenerator.Metadata;

public record ItemToGenerate(
    string Name,
    string NameSpace,
    string Accessibility,
    IEnumerable<ParameterDefinition> ConstructorParameters)
{
    public string Name { get; } = Name;
    public string NameSpace { get; } = NameSpace;
    public string Accessibility { get; } = Accessibility;
    public string FullyQualifiedName { get; } = $"{NameSpace}.{Name}";
    public IEnumerable<ParameterDefinition> ConstructorParameters { get; } = ConstructorParameters;
}