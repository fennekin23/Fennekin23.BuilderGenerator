using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class CodeStringBuilder
{
    private readonly StringBuilder _stringBuilder;
    private readonly int _indent;

    private CodeStringBuilder(StringBuilder stringBuilder, int indent)
    {
        _stringBuilder = stringBuilder;
        _indent = indent;
    }

    public static CodeStringBuilder Create(int capacity) => new(new StringBuilder(capacity), 0);

    public CodeStringBuilder Indent() => new CodeStringBuilder(_stringBuilder, _indent + 4);
    
    public void Append(string value)
    {
        _stringBuilder.Append(value);
    }
    
    public void AppendIndented(string value)
    {
        _stringBuilder.Append(' ', _indent);
        _stringBuilder.Append(value);
    }
    
    public void AppendLine()
    {
        _stringBuilder.AppendLine();
    }
    
    public void AppendLine(string value)
    {
        _stringBuilder.AppendLine(value);
    }
    
    public void AppendLineIndented(string value)
    {
        _stringBuilder.Append(' ', _indent);
        _stringBuilder.AppendLine(value);
    }

    public override string ToString() => _stringBuilder.ToString();
}