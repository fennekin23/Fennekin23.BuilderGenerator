// See https://aka.ms/new-console-template for more information

using Fennekin23.BuilderGenerator.Sample;

var sample = new SamplePublicRecordBuilder()
    .WithStringProperty("abcdef")
    .WithIntProperty(123456)
    .Build();

Console.WriteLine(sample);