using System;

namespace Calculator
{
    internal class ExpressionChecker
    {
        private const char plus = '+';
        private const char minus = '-';
        private const char multiply = '*';
        private const char divide = '/';
        private const char exponent = '^';
        private const char openParen = '(';
        private const char closedParen = ')';
        private const char decimalPoint = '.';

        public static void CharacterChecker(string input)
        {            
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '0' && input[i] != '1' && input[i] != '2' && input[i] != '3' &&
                    input[i] != '4' && input[i] != '5' && input[i] != '6' && input[i] != '7' &&
                    input[i] != '8' && input[i] != '9' && input[i] != decimalPoint && input[i] != plus &&
                    input[i] != minus && input[i] != multiply && input[i] != divide && input[i] != exponent &&
                    input[i] != openParen && input[i] != closedParen)
                {
                    throw new Exception("The expression can only have numbers and arithmetic operators.");
                }
            }
        }


        public static void FormatChecker(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[0] == plus || input[0] == minus || input[0] == multiply || input[0] == divide ||
                    input[0] == exponent || input[0] == closedParen || input[input.Length - 1] == plus ||
                    input[input.Length - 1] == minus || input[input.Length - 1] == multiply ||
                    input[input.Length - 1] == divide || input[input.Length - 1] == exponent ||
                    input[input.Length - 1] == openParen)
                {
                    throw new Exception("The expression cannot begin or end with an operator.");
                }
            }
        }


        public static void DecimalChecker(string input)
        {
            for (int j = 0; j < input.Length; j++)
            {
                if (input[j] == decimalPoint && ((j == 0 && input[j + 1] == openParen) ||
                    (input[j - 1] == closedParen || input[j + 1] == openParen) ||
                    (j == input.Length - 1 && input[j - 1] == closedParen)))
                {
                    throw new Exception("Invalid formatting");
                }
            }
        }
    }
}
