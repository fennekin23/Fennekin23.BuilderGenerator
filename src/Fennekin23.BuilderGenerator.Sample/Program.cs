using Fennekin23.BuilderGenerator.Sample;

var sample = new SamplePublicPositionalRecordBuilder()
    .WithStringProperty("Hello, world!")
    .WithIntProperty(int.MaxValue)
    .WithBooleanProperty(true)
    .WithLongProperty(long.MaxValue)
    .Build();

Console.WriteLine(sample);