# Fennekin23.BuilderGenerator

A Source Generator package that generates [builder](https://refactoring.guru/design-patterns/builder) classes for records, to allow convenient complex object building.

> This source generator requires the .NET 7 SDK. You can target earlier frameworks like .NET Core 3.1 etc, but the _SDK_ must be at least 7.0.100

To use the generator, add the `[BuilderGenerator]` attribute to a record. For example:

```csharp
[BuilderGenerator]
public record SamplePublicRecord(string StringProperty, int IntProperty);
```

This will generate a class called `SamplePublicRecordBuilder` (by default), which contains a number of helper methods. For example:

```csharp
public sealed partial class SamplePublicRecordBuilder
{
    private String _stringproperty = default!;
    public SamplePublicRecordBuilder WithStringProperty(String value)
    {
        _stringproperty = value;
        return this;
    }
    private Int32 _intproperty = default!;
    public SamplePublicRecordBuilder WithIntProperty(Int32 value)
    {
        _intproperty = value;
        return this;
    }
    public Fennekin23.BuilderGenerator.Sample.SamplePublicRecord Build()
    {
        return new Fennekin23.BuilderGenerator.Sample.SamplePublicRecord(StringProperty: _stringproperty, IntProperty: _intproperty);
    }
}
```