using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class MethodBuilder(
    string accessModifier,
    string returnType,
    string name,
    IEnumerable<KeyValuePair<string, string>> parameters,
    int indentLevel,
    StringBuilder builder)
    : CodeBuilderBase(indentLevel, builder), IDisposable
{
    private readonly int _baseIndentLevel = indentLevel;
    private readonly int _innerIndentLevel = indentLevel + 4;
    private readonly StringBuilder _builder = builder;

    public int IndentLevel => _baseIndentLevel;
    
    public void StartMethod()
    {
        _builder.AppendIndented(_baseIndentLevel, $"{accessModifier} {returnType} {name}");
        _builder.Append('(');
        foreach (var parameter in parameters)
        {
            _builder.Append($"{parameter.Key} {parameter.Value}");
        }
        _builder.Append(')');
        _builder.AppendLine();
        _builder.AppendLineIndented(_baseIndentLevel, "{");   
    }

    public void Body(params ReadOnlySpan<string> lines)
    {
        foreach (var line in lines)
        {
            _builder.AppendLineIndented(_innerIndentLevel, line);
        }
    }

    public void ReturnObjectResult(Action<ObjectBuilder> buildObject)
    {
        _builder.AppendLineIndented(_innerIndentLevel, "return");
        
        ObjectBuilder objectBuilder = new(_innerIndentLevel, _builder);
        buildObject(objectBuilder);

        _builder.AppendLine(";");
    }

    public void Return(params ReadOnlySpan<string> lines)
    {
        _builder.AppendIndented(_innerIndentLevel, "return ");
        _builder.AppendLine(lines[0]);
        if (lines.Length > 1)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                _builder.AppendLineIndented(_innerIndentLevel + 4, lines[i]);
            }
        }
    }

    public void Dispose() => _builder.AppendLineIndented(_baseIndentLevel, "}");
}