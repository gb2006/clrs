using System;

namespace Permutations
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.GeneratePermutations("abc");
        }

        // Generate all permutations of a string containing only unique characters
        // If string contains dupe characters, we need to use a hashtable to store
        // all generated values and check if a value has already been generated
        //
        void GeneratePermutationsUnique(char[] str, int currIndex)
        {
            if (currIndex == str.Length)
            {
                Console.WriteLine(str);
                return;
            }

            for (int i = currIndex; i < str.Length; i++)
            {
                Swap(str, i, currIndex);
                GeneratePermutationsUnique(str, currIndex + 1);
                Swap(str, i, currIndex);
            }
        }

        void GeneratePermutations(string s)
        {
            char[] str = s.ToCharArray();
            GeneratePermutationsUnique(str, 0);
        }

        void Swap(char[] str, int src, int dest)
        {
            char temp = str[src];
            str[src] = str[dest];
            str[dest] = temp;
        }
    }
}
