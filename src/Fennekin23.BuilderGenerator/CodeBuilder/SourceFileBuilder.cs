namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class SourceFileBuilder(int estimatedCapacity)
{
    private readonly CodeStringBuilder _builder = CodeStringBuilder.Create(estimatedCapacity);

    public void AddHeader()
    {
        _builder.AppendLine(
            """
            //------------------------------------------------------------------------------
            // <auto-generated>
            //     This code was generated by the Fennekin23.BuilderGenerator source generator.
            //     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
            // </auto-generated>
            //------------------------------------------------------------------------------
            """);
    }

    public void EnableNullable() => _builder.AppendLine("#nullable enable");

    public void AddNamespace(Action<NamespaceDefinitionBuilder>? buildNamespace)
    {
        if (buildNamespace is not null)
        {
            NamespaceDefinitionBuilder namespaceBuilder = new(_builder);
            buildNamespace(namespaceBuilder);
        }
    }

    public string Build() => _builder.ToString();
}