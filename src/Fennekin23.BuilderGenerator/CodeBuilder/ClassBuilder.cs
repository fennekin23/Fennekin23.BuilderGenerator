namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class ClassDefinitionBuilder(CodeStringBuilder builder)
{
    public ClassDefinitionBuilder WithComment(string comment)
    {
        builder.AppendLine(comment);
        return this;
    }

    public ClassDefinitionBuilder WithAttribute(string attribute)
    {
        builder.AppendLineIndented(attribute);
        return this;
    }
    
    public ClassDefinitionBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(accessModifier);
        builder.Append(" ");
        return this;
    }
    
    public ClassDefinitionBuilder WithName(string name)
    {
        builder.AppendLine($"sealed class {name}");
        return this;
    }

    public void WithBody(Action<ClassBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented("{");
        if (buildBody is not null)
        {
            ClassBodyBuilder bodyBuilder = new(builder.Indent());
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented("}");
    }
    
    public class ClassBodyBuilder(CodeStringBuilder builder)
    {
        public void AddField(Action<FieldBuilder> buildField)
        {
            FieldBuilder fieldBuilder = new(builder);
            buildField(fieldBuilder);

            builder.AppendLine(";");
        }

        public void AddMethod(Action<MethodDefinitionBuilder> buildMethod)
        {
            MethodDefinitionBuilder methodBuilder = new(builder);
            buildMethod(methodBuilder);
        }
    }
}