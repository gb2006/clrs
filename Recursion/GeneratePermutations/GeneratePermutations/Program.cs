using System;

namespace GeneratePermutations
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] arr = { 'a', 'b', 'c', 'd' };
            GeneratePermutations(arr);            
        }

        static void GeneratePermutations(char[] arr)
        {
            GeneratePermutations(arr, 0, arr.Length - 1);
        }

        static void GeneratePermutations(char[] arr, int st, int en)
        {
            if (st > en)
            {
                Console.WriteLine(arr);
                return;
            }

            for (int i = st; i <= en; i++)
            {
                Swap(arr, i, st);
                GeneratePermutations(arr, st + 1, en);
                Swap(arr, i, st);
            }
        }

        static void Swap(char[] arr, int p1, int p2)
        {
            char temp = arr[p1];
            arr[p1] = arr[p2];
            arr[p2] = temp;
        }
    }
}
