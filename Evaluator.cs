using System;

namespace Calculator
{
    internal class Evaluator
    {
        string result;
        private float value1;
        private float value2;
        private const char plus = '+';
        private const char minus = '-';
        private const char multiply = '*';
        private const char divide = '/';
        private const char exponent = '^';
        private const char openParen = '(';
        private const char closedParen = ')';        

        public string Calculate(string input)
        {
            result = input;
            ParenthesisEvaluator(result);
            ExponentEvaluator(result);
            MultiplicationEvaluator(result);
            DivisionEvaluator(result);
            AdditionEvaluator(result);
            SubtractionEvaluator(result);
            return result;
        }


        public void ParenthesisEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == openParen)
                {
                    int openParenCount = 0;
                    int closedParenCount = 0;
                    string parenthesisExpression = "";
                    string expressionBegin = "";
                    string expressionEnd = "";                    

                    // Find correct index of ')'
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (input[j] == openParen)
                        {
                            openParenCount++;
                        }
                        else if (input[j] == closedParen)
                        {
                            closedParenCount++;
                            if (openParenCount == closedParenCount)
                            {                                
                                parenthesisExpression = input.Substring(i + 1, j - i - 1);
                                expressionBegin = input.Substring(0, i);
                                expressionEnd = input.Substring(j + 1, (input.Length - 1) - j);
                                break;
                            }
                        }
                    }
                    
                    // Checks for incorrect formatting
                    ExpressionChecker.FormatChecker(parenthesisExpression);                    
                    
                    result = expressionBegin + Calculate(parenthesisExpression) + expressionEnd;

                    // Checks again for parenthesis
                    ParenthesisEvaluator(result);
                    break;
                }
            }            
        }


        public void ExponentEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == exponent)
                {
                    string expressionBegin = "";
                    string expressionEnd = "";

                    // Finding the value to the left of '^'
                    // Stops once finds an operator or the beginning
                    for (int j = i - 1; j >= 0; j--)
                    {
                        // If value1 is at the beginning or there's a negative sign
                        if (j == 0 || (input[j] == minus && (j == 0 || input[j - 1] == plus || 
                            input[j - 1] == minus || input[j - 1] == multiply || input[j - 1] == divide)))
                        {
                            value1 = Convert.ToSingle(input.Substring(j, i - j));
                            expressionBegin = input.Substring(0, j);
                            break;
                        }
                        // If an operator precedes value1
                        else if (input[j] == plus || input[j] == minus || input[j] == multiply || input[j] == divide)
                        {
                            value1 = Convert.ToSingle(input.Substring(j + 1, i - j - 1));
                            expressionBegin = input.Substring(0, j + 1);
                            break;
                        }                        
                    }

                    // Finding the value to the right of '^'
                    // Stops once finds an operator or the end
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        // If there's a negative sign
                        if (input[j] == minus && input[j - 1] == exponent)
                        {
                            for (int k = j + 1; k < input.Length; k++)
                            {
                                // If an operator follows value2
                                if (input[k] == plus || input[k] == minus || input[k] == multiply ||
                                    input[k] == divide || input[k] == exponent)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j));
                                    expressionEnd = input.Substring(k, input.Length - k);
                                    break;
                                }
                                // If value2 is at the end
                                else if (k == input.Length - 1)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j + 1));
                                }
                            }
                        }
                        // If an operator follows value2
                        else if (input[j] == plus || input[j] == minus || input[j] == multiply || 
                                input[j] == divide || input[j] == exponent)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i - 1));
                            expressionEnd = input.Substring(j, input.Length - j);
                            break;
                        }           
                        // If value 2 is at the end
                        else if (j == input.Length - 1)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i));                            
                        }
                    }

                    string exponentValue = Math.Pow(value1, value2).ToString();
                    result = expressionBegin + exponentValue + expressionEnd;

                    // Checks again for '^'
                    ExponentEvaluator(result); 
                    break;
                }
            }            
        }


        public void MultiplicationEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == multiply)
                {
                    string expressionBegin = "";
                    string expressionEnd = "";

                    // Finding the value to the left of '*'
                    // Stops once finds an operator or the beginning
                    for (int j = i - 1; j >= 0; j--)
                    {
                        // If value1 is at the beginning or there's a negative sign 
                        if (j == 0 || (input[j] == minus && (j == 0 || input[j - 1] == plus ||
                            input[j - 1] == minus || input[j - 1] == divide)))
                        {
                            value1 = Convert.ToSingle(input.Substring(j, i - j));
                            expressionBegin = input.Substring(0, j);
                            break;
                        }
                        // If an operator precedes value1
                        else if (input[j] == plus || input[j] == minus || input[j] == divide)
                        {
                            value1 = Convert.ToSingle(input.Substring(j + 1, i - j - 1));
                            expressionBegin = input.Substring(0, j + 1);
                            break;
                        }
                    }

                    // Finding the value to the right of '*'
                    // Stops once finds an operator or the end
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        // If there's a negative sign
                        if (input[j] == minus && input[j - 1] == multiply)
                        {
                            for (int k = j + 1; k < input.Length; k++)
                            {
                                // If an operator follows value2
                                if (input[k] == plus || input[k] == minus ||
                                    input[k] == divide || input[k] == exponent)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j));
                                    expressionEnd = input.Substring(k, input.Length - k);
                                    break;
                                }
                                // If value2 is at the end
                                else if (k == input.Length - 1)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j + 1));
                                }
                            }
                        }
                        // If an operator follows value2
                        else if (input[j] == plus || input[j] == minus || input[j] == multiply ||
                                input[j] == divide || input[j] == exponent)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i - 1));
                            expressionEnd = input.Substring(j, input.Length - j);
                            break;
                        }
                        // If value 2 is at the end
                        else if (j == input.Length - 1)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i));
                        }
                    }

                    string product = (value1 * value2).ToString();
                    result = expressionBegin + product + expressionEnd;

                    // Checks again for '*'
                    MultiplicationEvaluator(result);
                    break;
                }                
            }
        }


        public void DivisionEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {                
                if (input[i] == divide)
                {
                    string expressionBegin = "";
                    string expressionEnd = "";

                    // Finding the value to the left of '/'
                    // Stops once finds an operator or the beginning
                    for (int j = i - 1; j >= 0; j--)
                    {
                        // If value1 is at the beginning or there's a negative sign 
                        if (j == 0 || (input[j] == minus && (j == 0 || input[j - 1] == plus ||
                            input[j - 1] == minus)))
                        {
                            value1 = Convert.ToSingle(input.Substring(j, i - j));
                            expressionBegin = input.Substring(0, j);
                            break;
                        }
                        // If an operator precedes value1
                        else if (input[j] == plus || input[j] == minus)
                        {
                            value1 = Convert.ToSingle(input.Substring(j + 1, i - j - 1));
                            expressionBegin = input.Substring(0, j + 1);
                            break;
                        }
                    }

                    // Finding the value to the right of '/'
                    // Stops once finds an operator or the end
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        // If there's a negative sign
                        if (input[j] == minus && input[j - 1] == divide)
                        {
                            for (int k = j + 1; k < input.Length; k++)
                            {
                                // If an operator follows value2
                                if (input[k] == plus || input[k] == minus || input[k] == divide)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j));
                                    expressionEnd = input.Substring(k, input.Length - k);
                                    break;
                                }
                                // If value2 is at the end
                                else if (k == input.Length - 1)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j + 1));
                                }
                            }
                        }
                        // If an operator follows value2
                        else if (input[j] == plus || input[j] == minus || input[j] == divide)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i - 1));
                            expressionEnd = input.Substring(j, input.Length - j);
                            break;
                        }
                        // If value 2 is at the end
                        else if (j == input.Length - 1)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i));
                        }
                    }

                    string quotient = (value1 / value2).ToString();
                    result = expressionBegin + quotient + expressionEnd;

                    // Checks again for '/'
                    DivisionEvaluator(result);
                    break;
                }
            }            
        }


        public void AdditionEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == plus)
                {
                    string expressionBegin = "";
                    string expressionEnd = "";

                    // Finding the value to the left of plus
                    // Stops once finds an operator or the beginning
                    for (int j = i - 1; j >= 0; j--)
                    {
                        // If there's a negative sign or value1 is at the beginning
                        if (j == 0 || input[j] == minus && (j == 0 || input[j - 1] == plus))
                        {
                            value1 = Convert.ToSingle(input.Substring(j, i - j));
                            expressionBegin = input.Substring(0, j);
                            break;
                        }
                        // If there's a minus sign
                        else if (input[j] == minus)
                        {
                            value1 = Convert.ToSingle(input.Substring(j + 1, i - j - 1));
                            expressionBegin = input.Substring(0, j + 1);
                            break;
                        }                        
                    }                    

                    // Finding the value to the right of '+'
                    // Stops once finds an operator or the end
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        // If there's '-' and it's a negative sign
                        if (input[j] == minus && input[j - 1] == plus)
                        {
                            for (int k = j + 1; k < input.Length; k++)
                            {
                                if (input[k] == plus || input[k] == minus)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j));
                                    expressionEnd = input.Substring(k, input.Length - k);
                                    break;
                                }
                                else if (k == input.Length - 1)
                                {
                                    value2 = Convert.ToSingle(input.Substring(j, k - j + 1));
                                }
                            }
                        }
                        // If there's an operator
                        else if (input[j] == plus || input[j] == minus)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i - 1));
                            expressionEnd = input.Substring(j, input.Length - j);
                            break;
                        }
                        // If value2 is at the end
                        else if (j == input.Length - 1)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i));
                        }
                    }

                    string sum = (value1 + value2).ToString();
                    result = expressionBegin + sum + expressionEnd;

                    // Checks again for '+'
                    AdditionEvaluator(result);
                    break;
                }
            }
        }


        public void SubtractionEvaluator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {                
                if (input[i] == minus)
                {
                    string expressionEnd = "";

                    // If there's a negative sign
                    if (i == 0)
                    {
                        continue;
                    }

                    // The value to the left of the '-' 
                    value1 = Convert.ToSingle(input.Substring(0, i));

                    // Finds the value to the right of the '-'
                    // Stops once finds an operator or the end
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        if (input[j] == minus && input[j - 1] == minus)
                        {
                            continue;
                        }
                        if (input[j] == minus)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i - 1));
                            expressionEnd = input.Substring(j, input.Length - j);
                            break;
                        }
                        else if (j == input.Length - 1)
                        {
                            value2 = Convert.ToSingle(input.Substring(i + 1, j - i));
                        }
                    }
                    
                    string difference = (value1 - value2).ToString();
                    result = difference + expressionEnd;

                    // Checks again for '-'
                    SubtractionEvaluator(result);
                    break;
                }
            }            
        }     
    }
}
