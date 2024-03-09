namespace Fennekin23.BuilderGenerator.Metadata;

public record PropertyDefinition(string Name, string Type)
{
    public string Name { get; } = Name;
    public string Type { get; } = Type;
}