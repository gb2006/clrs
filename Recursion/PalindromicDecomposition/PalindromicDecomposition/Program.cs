using System;
using System.Collections.Generic;
using System.Text;

namespace PalindromicDecomposition
{
    class Program
    {
        /// <summary>
        /// Generate Palindromic Decompositions.
        /// Decompose a given string into substrings such that
        /// all those substrings are valid palindromes.
        /// For this, a single character substring is a valid
        /// palindrome.
        /// For example, for "abracadabra", all decompositions 
        /// are
        /// a|b|r|a|c|a|d|a|b|r|a|
        /// a|b|r|aca|d|a|b|r|a|
        /// a|b|r|a|c|ada|b|r|a|
        /// 
        /// Basically, the idea is to generate multiple 
        /// decompositions in such a way that within each
        /// decomposition each substring separated by a 
        /// delimiter (such as | here) is a valid palindrome
        /// </summary>
        static void Main(string[] args)
        {
            var list = GenerateDecompositions("abcbdefgf");
            foreach (var item in list)
                Console.WriteLine(item);
        }

        static string[] GenerateDecompositions(string str)
        {
            var output = new List<string>();
            var sb = new StringBuilder();

            GenerateDecompositions(str, 0, sb, output);
            return output.ToArray();
        }

        static void GenerateDecompositions(
            string str, 
            int idx, 
            StringBuilder sb, 
            List<string> output)
        {
            if (idx == str.Length)
            {
                output.Add(sb.ToString());
                return;
            }

            int len = sb.Length;
            for (int curr = idx; curr < str.Length; curr++)
            {
                if (IsPalindrome(str, idx, curr))
                {
                    for (int i = idx; i <= curr; i++)
                    {
                        sb.Append(str[i]);
                    }

                    // Add decomposition delimiter
                    //
                    sb.Append("|");
                    GenerateDecompositions(str, curr + 1, sb, output);
                    sb.Length = len;
                }
            }
        }

        static bool IsPalindrome(string s, int st, int en)
        {
            for (; st <= en; st++, en--)
            {
                if (s[st] != s[en]) return false;
            }
            return true;
        }
    }
}
