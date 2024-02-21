using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Fennekin23.BuilderGenerator.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Fennekin23.BuilderGenerator;

[Generator]
public class BuilderGenerator : IIncrementalGenerator
{
    private const string BuilderGeneratorAttribute = "Fennekin23.BuilderGenerator.BuilderGeneratorAttribute";
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "BuilderGeneratorAttribute.g.cs", SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));
        
        IncrementalValuesProvider<ItemToGenerate> itemsToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(BuilderGeneratorAttribute,
                (node, _) => node is RecordDeclarationSyntax,
                (syntaxContext, token) => GetItemToGenerate(syntaxContext, token))
            .WithTrackingName(TrackingNames.InitialExtraction)
            .Where(static m => m is not null)
            .WithTrackingName(TrackingNames.RemovingNulls)!;
        
        context.RegisterSourceOutput(itemsToGenerate,
            static (spc, itemToGenerate) => Execute(in itemToGenerate, spc));
    }

    private static ItemToGenerate? GetItemToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        var name = symbol.Name;
        var nameSpace = symbol.ContainingNamespace.IsGlobalNamespace ? string.Empty : symbol.ContainingNamespace.ToString();
        var accessibility = symbol.DeclaredAccessibility.ToString().ToLowerInvariant();
        var constructorParameters = new List<ParameterDefinition>();

        var constructor = FindConstructor(symbol);
        if (constructor?.Parameters != null)
        {
            foreach (var parameter in constructor.Parameters)
            {
                constructorParameters.Add(new ParameterDefinition(parameter.Name, parameter.Type.Name));
            }
        }

        return new ItemToGenerate(name, nameSpace, accessibility, constructorParameters);
    }

    private static IMethodSymbol? FindConstructor(INamedTypeSymbol symbol)
    {
        IMethodSymbol? constructor = null;
        int maxParameters = 0;

        foreach (var member in symbol.GetMembers())
        {
            if (member is not IMethodSymbol
                {
                    DeclaredAccessibility: Accessibility.Public,
                    MethodKind: MethodKind.Constructor
                } ctor)
                continue;

            if (ctor.Parameters.Length > maxParameters)
            {
                constructor = ctor;
                maxParameters = ctor.Parameters.Length;
            }
        }
        
        return constructor;
    }
    
    private static void Execute(in ItemToGenerate itemToGenerate, SourceProductionContext context)
    {
        StringBuilder sb = new();
        var result = SourceGenerationHelper.GenerateBuilderClass(sb, in itemToGenerate);
        context.AddSource(itemToGenerate.Name + "Builder.g.cs", SourceText.From(result, Encoding.UTF8));    
    }
}