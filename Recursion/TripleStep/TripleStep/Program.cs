using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleStep
{
    // A child is running up a staircase with n steps, can hop 1, 2, or 3 steps
    // at a time. Count how many possible ways the child can run up the stairs
    // Base cases:
    // If n < 0 -> 0 possible way
    // If n = 0 -> 1 possible way (this is convenience to avoid creating base steps for n=2 and n=3)
    // If n = 1 -> 1 possible way
    // Recurse
    // If n = 2 -> steps(n - 1) + steps(n - 2) + steps(n - 3) = 2
    // If n = 3 -> steps(n - 1) + steps(n - 2) + steps(n - 3) = 2 + 1 + 1 = 4
    //
    // This problem lands itself very well to be solved recursively as well
    // as through memoization technique
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CountSteps2(3));
        }

        // Recursive
        static int CountSteps(int n)
        {
            if (n < 0) return 0;
            if (n == 0 || n == 1) return 1;
            return CountSteps(n - 1) + CountSteps(n - 2) + CountSteps(n - 3);
        }

        // memoized (DP)
        static int CountSteps2(int n)
        {
            if (n < 0) return 0;

            int[] counts = new int[n + 1];
            counts[0] = 1;

            for (int i = 1; i <= n; i++)
            {
                counts[i] = counts[i - 1] +
                            ((i - 2 < 0) ? 0 : counts[i - 2]) +
                            ((i - 3 < 0) ? 0 : counts[i - 3]);
            }
            return counts[n];
        }
    }
}
