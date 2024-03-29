namespace Fennekin23.BuilderGenerator.Tests.Samples;

[BuilderGenerator]
public record SamplePublicPropertiesRecord
{
    public string? StringProperty { get; init; }
    
    public int IntProperty { get; set; }
    
    public bool BooleanProperty { get; }
}
