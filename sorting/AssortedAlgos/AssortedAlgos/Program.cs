using System;

namespace AssortedAlgos
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = GetTestArr();

            new SelectionSort().Sort(arr);
            Print(arr);

            new InsertionSort().Sort(arr);
            Print(arr);
        }

        static int[] GetTestArr()
        {
            Random r = new Random();
            int size = r.Next(10, 1000);

            int[] arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(10000);
            }

            return arr;
        }

        static void Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0} ", arr[i]);
            }
            Console.WriteLine();
        }
    }
}
