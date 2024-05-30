namespace Fennekin23.BuilderGenerator.Tests;

[BuilderGenerator]
public record SamplePublicPositionalClass(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }

    public long LongProperty { get; set; }
}
