using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class ObjectBuilder(
    int indentLevel,
    StringBuilder builder)
{
    private readonly int _baseIndentLevel = indentLevel + 4;

    public ObjectBuilder WithType(string typeName)
    {
        builder.AppendLineIndented(_baseIndentLevel, $"new {typeName}");
        return this;
    }

    public ObjectBuilder WithConstructorParameters(params KeyValuePair<string, string>[] parameters)
    {
        builder.AppendLineIndented(_baseIndentLevel, "(");
        if (parameters.Any())
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                builder.AppendIndented(_baseIndentLevel + 4, $"{param.Key}: {param.Value}");
                if (i < (parameters.Length - 1))
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine();
                }
            }
        }
        builder.AppendLineIndented(_baseIndentLevel, ")");

        return this;
    }

    public ObjectBuilder WithPropertiesInitializer(params ReadOnlySpan<KeyValuePair<string, string>> parameters)
    {
        builder.AppendLineIndented(_baseIndentLevel, "{");
        if (!parameters.IsEmpty)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                builder.AppendIndented(_baseIndentLevel + 4, $"{param.Key} = {param.Value}");
                if (i < (parameters.Length - 1))
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine();
                }
            }
        }
        builder.AppendIndented(_baseIndentLevel, "}");
        
        return this;
    }
}