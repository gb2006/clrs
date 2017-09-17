using System;
using System.Collections.Generic;

namespace SubsetsOfASet
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = GenerateSubsets("1234");
            foreach (var item in list)
                Console.WriteLine(item);
        }

        static string[] GenerateSubsets(string set)
        {
            var list = new List<string>();
            var sb = new System.Text.StringBuilder();

            // By default add empty set
            //
            list.Add(string.Empty);
            GenerateSubsets(set, 0, sb, list);

            return list.ToArray();
        }
        /// <summary>
        /// The key insight for this method is that at each element
        /// it is either part of the current subset, or not part of
        /// it. So at each element we call the function twice 
        /// recursively.
        /// </summary>
        static void GenerateSubsets(
            string set, 
            int idx, 
            System.Text.StringBuilder sb,
            List<string> list)
        {
            if (idx == set.Length)
            {
                list.Add(sb.ToString());
                return;
            }

            int len = sb.Length;

            // Add current element
            // Note: Comma is needed only when sb is not empty
            // 
            sb.Append(len > 0 ? "," : string.Empty);
            sb.Append(set[idx]);
            GenerateSubsets(set, idx + 1, sb, list);

            // reset buffer length to previous
            //
            sb.Length = len;

            // Skip current element
            //
            GenerateSubsets(set, idx + 1, sb, list);
        }

    }
}
