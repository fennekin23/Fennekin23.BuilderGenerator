using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public abstract class CodeBuilderBase(int indentLevel, StringBuilder builder)
{
    public void AddRaw(string value) => builder.AppendLineIndented(indentLevel, value);

    public void AddComment(string value) => builder.AppendLine(value);
}