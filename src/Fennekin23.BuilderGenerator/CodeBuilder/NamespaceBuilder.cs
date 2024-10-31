using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class NamespaceDefinitionBuilder(int indentLevel, StringBuilder builder)
{
    public NamespaceDefinitionBuilder WithName(string name)
    {
        builder.AppendLineIndented(indentLevel, $"namespace {name}");
        return this;
    }

    public void WithBody(Action<NamespaceBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented(indentLevel, "{");
        if (buildBody is not null)
        {
            NamespaceBodyBuilder bodyBuilder = new(indentLevel + 4, builder);
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented(indentLevel, "}");
    }

    public class NamespaceBodyBuilder(int indentLevel, StringBuilder builder)
    {
        public void AddClass(Action<ClassDefinitionBuilder> buildClass)
        {
            ClassDefinitionBuilder classBuilder = new(indentLevel, builder);
            buildClass(classBuilder);
        }
    }
}