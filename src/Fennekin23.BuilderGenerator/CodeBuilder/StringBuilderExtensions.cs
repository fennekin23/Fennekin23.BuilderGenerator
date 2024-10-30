using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public static class StringBuilderExtensions
{
    public static void AppendIndented(this StringBuilder builder, int indent, string value)
    {
        builder.Append(' ', indent);
        builder.Append(value);
    }
    
    public static void AppendLineIndented(this StringBuilder builder, int indent, string value)
    {
        builder.Append(' ', indent);
        builder.AppendLine(value);
    }
}