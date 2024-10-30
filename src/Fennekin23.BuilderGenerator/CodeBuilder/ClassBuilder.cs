using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class ClassBuilder(int indentLevel, StringBuilder builder)
    : CodeBuilderBase(indentLevel, builder), IDisposable
{
    private readonly int _indentLevel = indentLevel;
    private readonly StringBuilder _builder = builder;

    public void DefineClass(string accessModifier, string name)
    {
        _builder.AppendLineIndented(_indentLevel, $"{accessModifier} sealed class {name}");
        _builder.AppendLineIndented(_indentLevel, "{");   
    }
    
    public void DefinePartialClass(string accessModifier, string name)
    {
        _builder.AppendLineIndented(_indentLevel, $"{accessModifier} sealed partial class {name}");
        _builder.AppendLineIndented(_indentLevel, "{");   
    }

    public FieldBuilder CreateFieldBuilder(
        string accessModifier,
        string type,
        string name)
        => new(accessModifier, type, name, _indentLevel + 4, builder); 

    public MethodBuilder CreateMethodBuilder(
        string accessModifier,
        string returnType,
        string name,
        IEnumerable<KeyValuePair<string, string>>? parameters = null)
        => new(accessModifier, returnType, name, parameters ?? [], _indentLevel + 4, _builder);

    public void Dispose() => _builder.AppendLineIndented(_indentLevel, "}");
}