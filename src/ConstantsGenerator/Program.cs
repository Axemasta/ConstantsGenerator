using ConstantsGenerator;

namespace ConsoleApp;

public static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Starting...");

        Console.WriteLine($"Constants key one: {Constants.KeyOne}");
        Console.WriteLine($"Constants key two: {Constants.KeyTwo}");

        Console.WriteLine("Press any key to exit...");
        _ = Console.ReadKey();
    }
}