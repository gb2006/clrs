using System;
using System.Collections.Generic;
using System.Linq;

namespace ExprGenerators
{
    /// <summary>
    /// Given a string containing digits and a target number K, 
    /// generate all possible expresssions that evaluate to that
    /// value K. In order to generate expressions, we're given
    /// three operators. 1) concat 2) multiply(*) and 3) add (+)
    /// Concat operator combines two digits to make new number,
    /// such as "2"concat"3" gives "23". Multiply and add have
    /// usual meaning.  The resulting expression should be valid
    /// infix expression that follows regular precedence between
    /// * and +.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var listOfExpr = expressionCreator("123", 7);
            foreach (var item in listOfExpr)
            {
                Console.WriteLine(item);
            }
        }

        static string[] expressionCreator(string s, int k)
        {
            List<string> list = new List<string>();
            string[] operators = new string[] { string.Empty, "*", "+" };
            var exprSofar = new System.Text.StringBuilder();

            GenerateExpr(operators, s, 0, exprSofar, k, list);
            return list.ToArray();
        }

        /// <summary>
        /// The main insight of this recursion is this:
        /// Take current character in the string and add it to
        /// the expression so far using one of the three operators.
        /// This results in 3 new expressions, and for each of
        /// these 3 expressions, call the function again with the
        /// new expression with rest of the string.
        /// </summary>
        static void GenerateExpr(
            string[] operators,
            string s,
            int idx,
            System.Text.StringBuilder exprSofar,
            int k,
            List<string> list)
        {
            if (idx == s.Length)
            {
                string expr = exprSofar.ToString();
                if (EvaluateExpr(expr, k))
                {
                    list.Add(expr);
                }
            }
            else
            {
                int len = exprSofar.Length;

                // For index 0, we don't need to add operators because exprSofar is empty
                //
                if (idx == 0)
                {
                    exprSofar = exprSofar.Append(s[idx]);
                    GenerateExpr(operators, s, idx + 1, exprSofar, k, list);
                    exprSofar.Length = len;
                }
                else
                {
                    foreach (string op in operators)
                    {
                        exprSofar = exprSofar.Append(op).Append(s[idx]);
                        GenerateExpr(operators, s, idx + 1, exprSofar, k, list);
                        exprSofar.Length = len;
                    }
                }
            }
        }

        static bool EvaluateExpr(string expr, int k)
        {
            var tokens = System.Text.RegularExpressions.Regex.Split(expr, @"([*+])");
            var postfix = InfixToPostfix(tokens);

            return EvaluatePostfix(postfix) == k;
        }

        static int EvaluatePostfix(string[] postfix)
        {
            var s = new Stack<int>();
            foreach (var t in postfix)
            {
                if (t[0] == '+' || t[0] == '*')
                {
                    int operand2 = s.Pop();
                    int operand1 = s.Pop();

                    if (t[0] == '+')
                    {
                        s.Push(operand1 + operand2);
                    }
                    else if (t[0] == '*')
                    {
                        s.Push(operand1 * operand2);
                    }
                }
                else
                {
                    s.Push(Int32.Parse(t));
                }
            }

            return s.Pop();
        }

        static string[] InfixToPostfix(string[] tokens)
        {
            var list = new List<string>();
            var s = new Stack<string>();

            foreach (var t in tokens)
            {
                if (t[0] == '+' || t[0] == '*')
                {
                    while (s.Any() && IsLeftHigherThanRight(s.Peek(), t))
                    {
                        list.Add(s.Pop());
                    }

                    s.Push(t);
                }
                else
                {
                    list.Add(t);
                }
            }

            while (s.Any())
            {
                list.Add(s.Pop());
            }

            return list.ToArray();
        }

        static bool IsLeftHigherThanRight(string operator1, string operator2)
        {
            if (operator1[0] == '*') return true;
            if (operator2[0] == '*') return false;
            return true;
        }

    }
}
