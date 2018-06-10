using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAllPairsOfParens
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintParens(8, 0, "");
        }

        // code to print all valid (properly opened and closed)
        // combinations of n pairs of parentheses
        // n = 3
        // ((())), (()()), (())(), ()(()), ()()()

        static void PrintParens(int numToPlace, int numOpen, string s)
        {
            if (numOpen < 0) return;
            if (numToPlace == 0)
            {
                if (numOpen == 0)
                {
                    Console.WriteLine(s);
                }
                return;
            }

            PrintParens(numToPlace - 1, numOpen + 1, s + "(");
            PrintParens(numToPlace - 1, numOpen - 1, s + ")");
        }
    }
}
