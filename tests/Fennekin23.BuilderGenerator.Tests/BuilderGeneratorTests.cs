namespace Fennekin23.BuilderGenerator.Tests;

public class BuilderGeneratorTests
{
    [Theory]
    [MemberData(nameof(RecordSamples))]
    public Task ShouldGenerateBuilderForRecord(string sampleName, string input)
    {
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<BuilderGenerator>(input);

        Assert.Empty(diagnostics);
        return Verify(output).UseDirectory("Snapshots").UseFileName(sampleName);
    }

    public static TheoryData<string, string> RecordSamples()
    {
        TheoryData<string, string> theoryData = new();
        
        var testsDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        
        if (testsDirectory == null) return theoryData;
        
        var samplesDirectory = Path.Combine(testsDirectory, "Samples");
        var samples = Directory.EnumerateFiles(samplesDirectory, "*Record.cs").Select(i => new FileInfo(i));
        
        foreach (var sample in samples)
        {
            theoryData.Add(sample.Name, File.ReadAllText(sample.FullName));
        }

        return theoryData;
    }
}