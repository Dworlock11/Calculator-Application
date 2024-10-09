using System;
using System.Linq;

namespace Calculator
{
    internal class Programh
    {
        static void Main(string[] args)
        {            
            Evaluator evaluator = new Evaluator();
            Console.WriteLine("Enter an expression that uses basic arithmetic operators." + "\n" +
                              "When done, hit enter." + "\n");

            // Keeps running until an empty string is inputted
            while (true)
            {
                string input = Console.ReadLine().Trim();
                if (input == "")
                {
                    break;
                }

                // Checks for invalid characters
                ExpressionChecker.CharacterChecker(input);

                // Checks for operators at the beginning or end
                ExpressionChecker.FormatChecker(input);

                // Checks for expressions before or after decimal points (8-5).68
                ExpressionChecker.DecimalChecker(input);

                Console.WriteLine(evaluator.Calculate(input) + '\n');
            }
        }        
    }
}
