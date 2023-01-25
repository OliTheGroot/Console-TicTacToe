using System;

namespace TestConsole
{
    public static class ParsingHelper
    {
        public static int ParsingNumber()
        {
            while (true)
            {
                string playerInput = Console.ReadLine();

                if (int.TryParse(playerInput, out int parsedInput))
                {
                    return parsedInput;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input. Try again.");
                Console.ResetColor();
            }
        }
    }
}