using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
namespace ConstantsGenerator.SourceGenerators;

[Generator]
public class ConstantSourceGenerator : ISourceGenerator
{
    private readonly static Dictionary<string, string> ConstantsKeys = new Dictionary<string, string>()
    {
        { "KeyOne", "From source 1" },
        { "KeyTwo", "Lalalala lalalaala" },
    };

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // var constantsFile = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("Constants.json"));
        //
        // if (constantsFile is null)
        // {
        //     Console.WriteLine("Constants file not found");
        //     return;
        // }
        //
        // var fileContents = constantsFile.GetText(context.CancellationToken);
        //
        // if (fileContents is null)
        // {
        //     Console.WriteLine("File contents could not be read");
        //     return;
        // }
        //
        // var constants = JsonSerializer.Deserialize<Dictionary<string, string>>(constantsFile.)

        // Build up the source code 
        var constantTemplate = "public const string {0} = \"{1}\"";
 
        var formattedProperties = new List<string>(); 
 
        foreach (var kvp in  ConstantsKeys) 
        { 
            var camelKey = kvp.Key;
 
            var template = string.Format(constantTemplate, camelKey);

            formattedProperties.Add(template); 
        }

        var source = """
            namespace ConstantsGenerator;

            public static class Constants
            {
            """;

        var sb = new StringBuilder();

        sb.Append(source);
        
        var newLine = @"
";

        var constantsLines = string.Join(newLine, formattedProperties);

        sb.Append(constantsLines);

        sb.Append("}");

        context.AddSource("Constants.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }
}