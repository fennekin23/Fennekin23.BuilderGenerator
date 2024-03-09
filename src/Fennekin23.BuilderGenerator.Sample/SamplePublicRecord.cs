namespace Fennekin23.BuilderGenerator.Sample;

[BuilderGenerator]
public record SamplePublicRecord(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }
    
    public long LongProperty { get; set; }
}