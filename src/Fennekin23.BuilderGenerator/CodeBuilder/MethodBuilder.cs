using Fennekin23.BuilderGenerator.Metadata;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class MethodDefinitionBuilder(CodeStringBuilder builder)
{
    public MethodDefinitionBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(accessModifier);
        builder.Append(" ");
        return this;
    }
    
    public MethodDefinitionBuilder WithReturnType(string typeName)
    {
        builder.Append(typeName);
        builder.Append(" ");
        return this;
    }
    
    public MethodDefinitionBuilder WithName(string name)
    {
        builder.Append(name);
        return this;
    }

    public MethodDefinitionBuilder WithParameter((TypeDefinition Type, string Name) parameter)
    {
        builder.Append("(");
        builder.Append(parameter.Type.Name);
        if (parameter.Type.IsNullable)
        {
            builder.Append("?");
        }
        builder.Append(" ");
        builder.Append(parameter.Name);
        builder.Append(")");
        builder.AppendLine();
        return this;
    }
    
    public MethodDefinitionBuilder WithoutParameters()
    {
        builder.AppendLine("()");
        return this;
    }
    
    public void WithBody(Action<MethodBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented("{");
        if (buildBody is not null)
        {
            MethodBodyBuilder bodyBuilder = new (builder.Indent());
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented("}");
    }

    public class MethodBodyBuilder(CodeStringBuilder builder)
    {
        public void WithStatements(string[] statements, Action<ReturnBuilder>? buildResult)
        {
            foreach (var statement in statements)
            {
                builder.AppendLineIndented(statement);
            }

            if (buildResult is not null)
            {
                ReturnBuilder returnBuilder = new(builder);
                buildResult(returnBuilder);
            }
        }
        
        public class ReturnBuilder(CodeStringBuilder builder)
        {
            public void WithSelfResult()
            {
                builder.AppendLineIndented("return this;");
            }
        
            public void WithObjectResult(Action<ObjectBuilder> buildObject)
            {
                builder.AppendLineIndented("return");
        
                ObjectBuilder objectBuilder = new(builder.Indent());
                buildObject(objectBuilder);

                builder.AppendLine(";");
            }
        }
    }
}