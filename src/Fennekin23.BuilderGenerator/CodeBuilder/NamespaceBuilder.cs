using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class NamespaceBuilder(int indentLevel, StringBuilder builder)
    : CodeBuilderBase(indentLevel, builder), IDisposable
{
    private readonly int _indentLevel = indentLevel;
    private readonly StringBuilder _builder = builder;
    
    public void DefineNamespace(string definition)
    {
        _builder.AppendLineIndented(_indentLevel, $"namespace {definition}");
        _builder.AppendLineIndented(_indentLevel, "{");
    }

    public ClassBuilder CreateClassBuilder() => new(_indentLevel + 4, _builder);

    public void Dispose() => _builder.AppendLineIndented(_indentLevel, "}");
}