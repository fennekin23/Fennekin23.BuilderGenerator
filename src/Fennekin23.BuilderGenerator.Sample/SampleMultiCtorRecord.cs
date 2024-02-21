namespace Fennekin23.BuilderGenerator.Sample;

[BuilderGenerator]
public record SampleMultiCtorRecord
{
    public string StringProperty { get; }
    public int IntProperty { get; }

    public SampleMultiCtorRecord(string stringProperty)
    {
        StringProperty = stringProperty;
    }

    public SampleMultiCtorRecord(string stringProperty, int intProperty)
    {
        StringProperty = stringProperty;
        IntProperty = intProperty;
    }
}