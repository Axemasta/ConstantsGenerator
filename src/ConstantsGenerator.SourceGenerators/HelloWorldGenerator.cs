using Microsoft.CodeAnalysis;
namespace ConstantsGenerator.SourceGenerators;

// Following this guide!
// https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Find the main method
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        if (mainMethod is null)
        {
            return;
        }
        
        // Build up the source code
        string source = $@"// <auto-generated/>
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public static partial class {mainMethod.ContainingType.Name}
    {{
        static partial void HelloFrom(string name) =>
            Console.WriteLine($""Generator says: Hi from '{{name}}'"");
    }}
}}
";
        var typeName = mainMethod.ContainingType.Name;

        // Add the source code to the compilation
        context.AddSource($"{typeName}.g.cs", source);
    }
}