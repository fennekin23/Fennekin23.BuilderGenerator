namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class FieldBuilder(CodeStringBuilder builder)
{
    public FieldBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(accessModifier);
        builder.Append(" ");
        return this;
    }
    
    public FieldBuilder WithType(string typeName)
    {
        builder.Append(typeName);
        builder.Append(" ");
        return this;
    }
    
    public FieldBuilder WithName(string name)
    {
        builder.Append(name);
        builder.Append(" ");
        return this;
    }
    
    public void WithValue(string? value = null)
    {
        builder.Append($"= {value ?? "default!"}");
    }
}