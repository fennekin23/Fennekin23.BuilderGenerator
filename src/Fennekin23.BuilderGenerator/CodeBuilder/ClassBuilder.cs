using System.Text;

namespace Fennekin23.BuilderGenerator.CodeBuilder;

public class ClassDefinitionBuilder(int indentLevel, StringBuilder builder)
{
    public ClassDefinitionBuilder WithComment(string comment)
    {
        builder.AppendLine(comment);
        return this;
    }

    public ClassDefinitionBuilder WithAttribute(string attribute)
    {
        builder.AppendLineIndented(indentLevel, attribute);
        return this;
    }
    
    public ClassDefinitionBuilder WithAccessModifier(string accessModifier)
    {
        builder.AppendIndented(indentLevel, accessModifier);
        builder.Append(' ');
        return this;
    }
    
    /*public ClassDefinitionBuilder WithModifiers(params ReadOnlySpan<string> modifiers)
    {
        if (!modifiers.IsEmpty)
        {
            foreach (var modifier in modifiers)
            {
                builder.Append(modifier);
                builder.Append(' ');
            }
        }
        
        return this;
    }*/
    
    public ClassDefinitionBuilder WithName(string name)
    {
        builder.AppendLine($"sealed class {name}");
        return this;
    }

    public void WithBody(Action<ClassBodyBuilder>? buildBody = null)
    {
        builder.AppendLineIndented(indentLevel, "{");
        if (buildBody is not null)
        {
            ClassBodyBuilder bodyBuilder = new(indentLevel + 4, builder);
            buildBody(bodyBuilder);
        }
        builder.AppendLineIndented(indentLevel, "}");
    }
    
    public class ClassBodyBuilder(int indentLevel, StringBuilder builder)
    {
        public void AddField(Action<FieldBuilder> buildField)
        {
            FieldBuilder fieldBuilder = new(indentLevel, builder);
            buildField(fieldBuilder);

            builder.AppendLine(";");
        }

        public void AddMethod(Action<MethodDefinitionBuilder> buildMethod)
        {
            MethodDefinitionBuilder methodBuilder = new(indentLevel, builder);
            buildMethod(methodBuilder);
        }
    }
}