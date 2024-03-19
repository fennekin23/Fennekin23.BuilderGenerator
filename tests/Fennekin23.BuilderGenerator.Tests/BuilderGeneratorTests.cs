namespace Fennekin23.BuilderGenerator.Tests;

public class BuilderGeneratorTests
{
    [Fact]
    public Task ShouldGenerateBuilderForPublicPositionalRecord()
    {
        const string input = 
            """
            namespace Fennekin23.BuilderGenerator;

            [BuilderGenerator]
            public record SamplePublicPositionalRecord(string StringProperty, int IntProperty)
            {
                public bool BooleanProperty { get; init; }
             
                public long LongProperty { get; set; }
            }
            """;
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<BuilderGenerator>(input);

        Assert.Empty(diagnostics);
        return Verify(output).UseDirectory("Snapshots");
    }
}