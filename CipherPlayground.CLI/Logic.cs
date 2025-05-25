using System;
using System.Runtime.CompilerServices;

namespace CipherPlayground.CLI
{
    internal class Logic
    {
        public static T GetUserInput<T>(
            string message,
            bool acceptEmpty = false,
            bool acceptWhitespaceOnly = false,
            uint? expectedLength = null,
            T? defaultValue = default,
            [CallerArgumentExpression("defaultValue")] string defaultValueExpression = "")
        {
            bool hasDefaultValue = !string.IsNullOrEmpty(defaultValueExpression);

            if (!message.EndsWith(" ")) { message += " "; }
            if (hasDefaultValue && defaultValue is not null)
            {
                message += $"[Default: {defaultValue}] ";
            }

            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Input cannot be null. Please make a valid input.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        // Only return defaultValue if caller actually supplied one
                        if (hasDefaultValue && defaultValue is not null)
                            return defaultValue;

                        if (!acceptEmpty)
                        {
                            Console.WriteLine("Input cannot be empty. Please make a valid input.");
                            continue;
                        }
                    }
                    else // whitespace but not empty
                    {
                        if (!acceptWhitespaceOnly)
                        {
                            Console.WriteLine("Input cannot be whitespace only. Please make a valid input.");
                            continue;
                        }
                    }
                }

                if (expectedLength != null && input!.Length != expectedLength)
                {
                    Console.WriteLine($"Input must be {expectedLength} characters long. Please make a valid input.");
                    continue;
                }

                switch (typeof(T))
                {
                    case Type t when t == typeof(string):
                        return (T)(object)input!;
                    case Type t when t == typeof(int):
                        if (int.TryParse(input, out int intResult))
                            return (T)(object)intResult;
                        Console.WriteLine("Invalid input. Please enter a valid whole number.");
                        break;
                    case Type t when t == typeof(double):
                        if (double.TryParse(input, out double doubleResult))
                            return (T)(object)doubleResult;
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        break;
                    case Type t when t == typeof(bool):
                        if (bool.TryParse(input, out bool boolResult))
                            return (T)(object)boolResult;
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
                        break;
                    case Type t when t.IsEnum:
                        if (Enum.TryParse(typeof(T), input, ignoreCase: true, out object? enumResult))
                            return (T)enumResult;
                        Console.WriteLine($"Invalid input. Please enter one of the following: {string.Join(", ", Enum.GetNames(typeof(T)))}.");
                        break;
                    default:
                        throw new NotSupportedException($"Type {typeof(T)} is not supported.");
                }
            }
        }
    }
}
