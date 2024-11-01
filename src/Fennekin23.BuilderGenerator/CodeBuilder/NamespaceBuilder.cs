namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class NamespaceDefinitionBuilder(CodeStringBuilder builder)
{
    public NamespaceDefinitionBuilder WithName(string name)
    {
        builder.AppendLineIndented($"namespace {name}");
        return this;
    }

    public void WithBody(Action<NamespaceBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented("{");
        if (buildBody is not null)
        {
            NamespaceBodyBuilder bodyBuilder = new(builder.Indent());
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented("}");
    }

    public class NamespaceBodyBuilder(CodeStringBuilder builder)
    {
        public void AddClass(Action<ClassDefinitionBuilder> buildClass)
        {
            ClassDefinitionBuilder classBuilder = new(builder);
            buildClass(classBuilder);
        }
    }
}