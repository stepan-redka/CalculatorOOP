using ClassLibrary;
using System.Globalization;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new MyCalculator();
            var evaluator = new ExpressionEvaluator(calculator);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Console Calculator ===");
                Console.WriteLine("1. Addition (a + b)");
                Console.WriteLine("2. Subtraction (a - b)");
                Console.WriteLine("3. Multiplication (a * b)");
                Console.WriteLine("4. Division (a / b)");
                Console.WriteLine("5. Evaluate Expression (3 + 4 * (2 - 1))");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                if (choice == "6")
                {
                    Console.WriteLine("Exiting... Goodbye!");
                    break;
                }

                //  OPTION 5: Full Expression
                if (choice == "5")
                {
                    Console.Clear();
                    Console.WriteLine("=== Expression Evaluator ===");
                    Console.WriteLine("Example: 3 + 4 * (2 - 1)");
                    Console.Write("Enter expression: ");

                    string expr = Console.ReadLine();

                    try
                    {
                        double result = evaluator.Evaluate(expr);
                        Console.WriteLine($"\nResult: {result}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }

                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    continue;
                }

                //  BASIC OPERATIONS
                string op = choice switch
                {
                    "1" => "+",
                    "2" => "-",
                    "3" => "*",
                    "4" => "/",
                    _ => null
                };

                if (op == null)
                {
                    Console.WriteLine("Invalid option! Press ENTER to try again.");
                    Console.ReadLine();
                    continue;
                }

                Console.Clear();
                Console.WriteLine($"You selected operation: {op}\n");

                double a = ReadDouble("Enter first number: ");
                double b = ReadDouble("Enter second number: ");

                try
                {
                    double result = calculator.Calculate(a, op, b);
                    Console.WriteLine($"\nResult: {result}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
            }
        }

        //  Number input
        private static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    input = input.Replace(',', '.');

                if (double.TryParse(input, out double value))
                    return value;

                Console.WriteLine("Invalid number! Please enter a valid numeric value.\n");
            }
        }
    }
}
