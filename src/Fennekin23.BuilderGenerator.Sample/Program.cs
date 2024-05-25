using Fennekin23.BuilderGenerator.Sample;

var sampleRecord = new SamplePublicPositionalRecordBuilder()
    .WithStringProperty("Hello, world!")
    .WithIntProperty(int.MaxValue)
    .WithBooleanProperty(true)
    .WithLongProperty(long.MaxValue)
    .Build();

var sampleClass = new SamplePublicPositionalClassBuilder()
    .WithStringProperty("Hello, world!")
    .WithIntProperty(int.MaxValue)
    .WithBooleanProperty(true)
    .WithLongProperty(long.MaxValue)
    .Build();

Console.WriteLine($"Record: {sampleRecord}");
Console.WriteLine($"Class: {sampleClass}");