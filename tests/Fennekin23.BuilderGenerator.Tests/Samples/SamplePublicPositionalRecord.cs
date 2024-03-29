namespace Fennekin23.BuilderGenerator.Tests.Samples;

[BuilderGenerator]
public record SamplePublicPositionalRecord(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }
             
    public long LongProperty { get; set; }
}