namespace Fennekin23.BuilderGenerator.Tests.Samples;

[BuilderGenerator]
public record SamplePublicConstructorRecord
{
    public string StringProperty { get; }
    public int IntProperty { get; }

    public SamplePublicConstructorRecord(string stringProperty)
    {
        StringProperty = stringProperty;
    }

    public SamplePublicConstructorRecord(string stringProperty, int intProperty)
    {
        StringProperty = stringProperty;
        IntProperty = intProperty;
    }
}