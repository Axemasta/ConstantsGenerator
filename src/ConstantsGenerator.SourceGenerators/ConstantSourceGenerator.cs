using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
namespace ConstantsGenerator.SourceGenerators;

[Generator]
public class ConstantSourceGenerator : ISourceGenerator
{
    private readonly static Dictionary<string, string> ConstantsKeys = new Dictionary<string, string>()
    {
        { "KeyOne", "From source 1" },
        { "KeyTwo", "Skip dip dip" },
    };

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Find main entry point
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        if (mainMethod is null)
        {
            return;
        }

        var source = GenerateSource(mainMethod.ContainingNamespace.ToDisplayString(), ConstantsKeys);

        context.AddSource("Constants.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string GenerateSource(string generatedNamespace, Dictionary<string, string> constantsToGenerate)
    {
        // Build up the source code 
        var constantTemplate = "public const string {0} = \"{1}\";";

        var formattedProperties = new List<string>();

        foreach (var kvp in constantsToGenerate)
        {
            var camelKey = kvp.Key;

            var template = string.Format(constantTemplate, camelKey, kvp.Value);

            formattedProperties.Add(template);
        }

        var source = $@"// <auto-generated/>
namespace {generatedNamespace};

public static class Constants
{{
";

        var sb = new StringBuilder();

        sb.Append(source);

        var newLine = @"
";

        sb.Append(newLine);

        int i = 0;

        foreach (var property in formattedProperties)
        {
            if (i > 0)
            {
                sb.AppendLine();
            }

            sb.Append(new string('\t', 1));

            sb.Append(property);

            i++;
        }

        sb.AppendLine();
        sb.AppendLine("}");

        return sb.ToString();
    }
}