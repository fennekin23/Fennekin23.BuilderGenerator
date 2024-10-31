using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class FieldBuilder(
    int indentLevel,
    StringBuilder builder)
{
    public FieldBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(indentLevel, accessModifier);
        builder.Append(' ');
        return this;
    }
    
    public FieldBuilder WithType(string typeName)
    {
        builder.Append(typeName);
        builder.Append(' ');
        return this;
    }
    
    public FieldBuilder WithName(string name)
    {
        builder.Append(name);
        builder.Append(' ');
        return this;
    }
    
    public FieldBuilder WithValue(string? value = null)
    {
        builder.Append($"= {value ?? "default!"}");
        return this;
    }
}