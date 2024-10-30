using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class FieldBuilder(
    string accessModifier,
    string type,
    string name,
    int indentLevel,
    StringBuilder builder)
    : CodeBuilderBase(indentLevel, builder), IDisposable
{
    private readonly int _baseIndentLevel = indentLevel;
    private readonly StringBuilder _builder = builder;

    public void StartField()
    {
        _builder.AppendLineIndented(_baseIndentLevel, $"{accessModifier} {type} {name} = default!;");
    }

    public void Dispose()
    {
    }
}