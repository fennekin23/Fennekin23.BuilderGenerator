namespace Fennekin23.BuilderGenerator.Sample;

[BuilderGenerator]
public record SamplePropRecord
{
    public string? StringProperty { get; init; }
    
    public int IntProperty { get; set; }
    
    public bool BooleanProperty { get; }
}