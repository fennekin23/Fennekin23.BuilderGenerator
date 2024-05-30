namespace Fennekin23.BuilderGenerator.Tests;

[BuilderGenerator]
internal record SampleInternalPositionalClass(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }

    public long LongProperty { get; set; }
}
