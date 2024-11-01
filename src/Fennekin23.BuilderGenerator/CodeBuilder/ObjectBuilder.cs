namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class ObjectBuilder(CodeStringBuilder builder)
{
    public ObjectBuilder WithType(string typeName)
    {
        builder.AppendLineIndented($"new {typeName}");
        return this;
    }

    public ObjectBuilder WithConstructorParameters(params (string Name, string Value)[] parameters)
    {
        builder.AppendLineIndented("(");
        if (parameters.Any())
        {
            CodeStringBuilder innerBuilder = builder.Indent();
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                innerBuilder.AppendIndented($"{param.Name}: {param.Value}");
                if (i < (parameters.Length - 1))
                {
                    innerBuilder.Append(",");
                }
                innerBuilder.AppendLine();
            }
        }
        builder.AppendLineIndented(")");

        return this;
    }

    public ObjectBuilder WithPropertiesInitializer(params (string Name, string Value)[] parameters)
    {
        builder.AppendLineIndented("{");
        if (parameters.Any())
        {
            CodeStringBuilder innerBuilder = builder.Indent();
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                innerBuilder.AppendIndented($"{param.Name} = {param.Value}");
                if (i < (parameters.Length - 1))
                {
                    innerBuilder.Append(",");
                }
                innerBuilder.AppendLine();
            }
        }
        builder.AppendIndented("}");
        
        return this;
    }
}