namespace ConsoleApp;

partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...");

        HelloFrom("Generated Code");

        Console.WriteLine("Press any key to exit...");
        _ = Console.ReadKey();
    }

    static partial void HelloFrom(string name);
}