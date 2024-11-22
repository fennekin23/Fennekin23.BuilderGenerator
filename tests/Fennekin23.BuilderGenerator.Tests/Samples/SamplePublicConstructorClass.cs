namespace Fennekin23.BuilderGenerator.Tests;

[BuilderGenerator]
public record SamplePublicConstructorClass
{
    public string StringProperty { get; }
    public int? IntProperty { get; }

    public SamplePublicConstructorClass(string stringProperty)
    {
        StringProperty = stringProperty;
    }

    public SamplePublicConstructorClass(string stringProperty, int? intProperty)
    {
        StringProperty = stringProperty;
        IntProperty = intProperty;
    }
}
