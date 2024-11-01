using System.Collections.Immutable;
using System.Text;
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
        string attributeSource = SourceGenerationHelper.GenerateAttributeClass();
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "BuilderGeneratorAttribute.g.cs", SourceText.From(attributeSource, Encoding.UTF8)));
        
        IncrementalValuesProvider<ItemToGenerate> itemsToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(BuilderGeneratorAttribute,
                (node, _) => node is RecordDeclarationSyntax || node is ClassDeclarationSyntax,
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

        ImmutableArray<ISymbol> symbolMembers = symbol.GetMembers();

        ct.ThrowIfCancellationRequested();
        
        IMethodSymbol? constructor = FindConstructor(symbolMembers);
        List<ParameterDefinition> constructorParametersDefinitions = GetConstructorParametersDefinitions(constructor);
        
        ct.ThrowIfCancellationRequested();
        
        List<IPropertySymbol> properties = FindProperties(symbolMembers);
        List<PropertyDefinition> propertiesDefinitions = GetPropertiesDefinitions(constructorParametersDefinitions, properties);

        return new ItemToGenerate(name,
            nameSpace,
            accessibility,
            constructorParametersDefinitions,
            propertiesDefinitions);
    }

    private static IMethodSymbol? FindConstructor(ImmutableArray<ISymbol> symbolMembers)
    {
        IMethodSymbol? constructor = null;
        int maxParameters = 0;

        foreach (var member in symbolMembers)
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

    private static List<ParameterDefinition> GetConstructorParametersDefinitions(IMethodSymbol? constructor)
    {
        return constructor?.Parameters == null
            ? []
            : constructor.Parameters
                .Select(p => new ParameterDefinition(p.Name, p.Type.Name))
                .ToList();
    }
    
    private static List<IPropertySymbol> FindProperties(ImmutableArray<ISymbol> symbolMembers)
    {
        List<IPropertySymbol> properties = [];
        
        foreach (var member in symbolMembers)
        {
            if (member is not IPropertySymbol
                {
                    DeclaredAccessibility: Accessibility.Public,
                    IsReadOnly: false,
                    SetMethod: { MethodKind: MethodKind.PropertySet }
                } prop)
                continue;
            
            properties.Add(prop);
        }
        
        return properties;
    }

    private static List<PropertyDefinition> GetPropertiesDefinitions(
        List<ParameterDefinition> constructorParametersDefinitions,
        List<IPropertySymbol> properties)
    {
        return properties
            .Where(property => !IsPositional(property))
            .Select(p => new PropertyDefinition(p.Name, p.Type.Name))
            .ToList();

        bool IsPositional(IPropertySymbol property)
            => constructorParametersDefinitions.Any(i => i.Name.Equals(property.Name));
    }
    
    private static void Execute(in ItemToGenerate itemToGenerate, SourceProductionContext context)
    {
        var result = SourceGenerationHelper.GenerateBuilderClass(in itemToGenerate);
        context.AddSource(itemToGenerate.Name + "Builder.g.cs", SourceText.From(result, Encoding.UTF8));
    }
}