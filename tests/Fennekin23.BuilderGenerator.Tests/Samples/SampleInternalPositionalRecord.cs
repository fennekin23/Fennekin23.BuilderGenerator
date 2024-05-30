namespace Fennekin23.BuilderGenerator.Tests.Samples;

[BuilderGenerator]
internal record SampleInternalPositionalRecord(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }

    public long LongProperty { get; set; }
}