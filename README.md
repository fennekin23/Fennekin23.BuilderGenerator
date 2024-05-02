# Fennekin23.BuilderGenerator

A Source Generator package that generates [builder](https://refactoring.guru/design-patterns/builder) classes for records, to allow convenient complex object building.

> This source generator requires the .NET 7 SDK. You can target earlier frameworks like .NET 6 etc, but the _SDK_ must be at least 7.0.100

To use the generator, add the `[BuilderGenerator]` attribute to a record. For example:

```csharp
[BuilderGenerator]
public record SamplePublicPositionalRecord(string StringProperty, int IntProperty)
{
    public bool BooleanProperty { get; init; }
             
    public long LongProperty { get; set; }
}
```

This will generate a class called `SamplePublicPositionalRecordBuilder` (by default), which contains a number of helper methods. For example:

```csharp
public sealed partial class SamplePublicPositionalRecordBuilder
{
    private String _stringproperty = default!;
    public SamplePublicPositionalRecordBuilder WithStringProperty(String value)
    {
        _stringproperty = value;
        return this;
    }
    private Int32 _intproperty = default!;
    public SamplePublicPositionalRecordBuilder WithIntProperty(Int32 value)
    {
        _intproperty = value;
        return this;
    }
    private Boolean _booleanproperty = default!;
    public SamplePublicPositionalRecordBuilder WithBooleanProperty(Boolean value)
    {
        _booleanproperty = value;
        return this;
    }
    private Int64 _longproperty = default!;
    public SamplePublicPositionalRecordBuilder WithLongProperty(Int64 value)
    {
        _longproperty = value;
        return this;
    }
    public Fennekin23.BuilderGenerator.Sample.SamplePublicPositionalRecord Build()
    {
        return new Fennekin23.BuilderGenerator.Sample.SamplePublicPositionalRecord(StringProperty: _stringproperty, IntProperty: _intproperty)
        {
            BooleanProperty = _booleanproperty,
            LongProperty = _longproperty
        };
    }
}
```