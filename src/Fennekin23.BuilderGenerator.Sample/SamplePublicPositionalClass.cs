namespace Fennekin23.BuilderGenerator.Sample;

[BuilderGenerator]
public record SamplePublicPositionalClass(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }

    public long LongProperty { get; set; }

    public override string ToString() =>
        $"{nameof(SamplePublicPositionalClass)}"
        +" {"
        + $" {nameof(StringProperty)} = {StringProperty},"
        + $" {nameof(IntProperty)} = {IntProperty},"
        + $" {nameof(BooleanProperty)} = {BooleanProperty},"
        + $" {nameof(LongProperty)} = {LongProperty}"
        +" }";
}