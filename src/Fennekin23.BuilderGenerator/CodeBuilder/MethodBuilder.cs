using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class MethodDefinitionBuilder(int indentLevel, StringBuilder builder)
{
    public MethodDefinitionBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(indentLevel, accessModifier);
        builder.Append(' ');
        return this;
    }
    
    public MethodDefinitionBuilder WithReturnType(string typeName)
    {
        builder.Append(typeName);
        builder.Append(' ');
        return this;
    }
    
    public MethodDefinitionBuilder WithName(string name)
    {
        builder.Append(name);
        return this;
    }

    public MethodDefinitionBuilder WithParameter(KeyValuePair<string, string> parameter)
    {
        builder.AppendLine($"({parameter.Key} {parameter.Value})");
        return this;
    }
    
    public MethodDefinitionBuilder WithoutParameters()
    {
        builder.AppendLine("()");
        return this;
    }
    
    public void WithBody(Action<MethodBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented(indentLevel, "{");
        if (buildBody is not null)
        {
            MethodBodyBuilder bodyBuilder = new MethodBodyBuilder(indentLevel + 4, builder);
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented(indentLevel, "}");
    }

    public class MethodBodyBuilder(int indentLevel, StringBuilder builder)
    {
        public void WithStatements(ReadOnlySpan<string> statements, Action<ReturnBuilder>? buildResult)
        {
            foreach (var statement in statements)
            {
                builder.AppendLineIndented(indentLevel, statement);
            }

            if (buildResult is not null)
            {
                ReturnBuilder returnBuilder = new(indentLevel, builder);
                buildResult(returnBuilder);
            }
        }
        
        public class ReturnBuilder(int indentLevel, StringBuilder builder)
        {
            public void WithSelfResult()
            {
                builder.AppendLineIndented(indentLevel, "return this;");
            }
        
            public void WithObjectResult(Action<ObjectBuilder> buildObject)
            {
                builder.AppendLineIndented(indentLevel, "return");
        
                ObjectBuilder objectBuilder = new(indentLevel, builder);
                buildObject(objectBuilder);

                builder.AppendLine(";");
            }
        }
    }
}