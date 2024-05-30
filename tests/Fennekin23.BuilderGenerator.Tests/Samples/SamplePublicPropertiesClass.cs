namespace Fennekin23.BuilderGenerator.Tests;

[BuilderGenerator]
public record SamplePublicPropertiesClass
{
    public string? StringProperty { get; init; }
    
    public int IntProperty { get; set; }
    
    public bool BooleanProperty { get; }
}
