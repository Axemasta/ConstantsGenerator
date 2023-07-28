namespace ConsoleApp;

partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...");

        Console.WriteLine($"Constants key one: {Constants.KeyOne}");
        Console.WriteLine($"Constants key two: {Constants.KeyTwo}");

        Console.WriteLine("Press any key to exit...");
        _ = Console.ReadKey();
    }
}