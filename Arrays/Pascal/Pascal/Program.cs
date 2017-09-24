using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pascal
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintPascalTriangle(12);
        }

        static void PrintPascalTriangle(int n)
        {
            if (n <= 0) return;

            int[] arr = new int[n];
            arr[0] = 1;

            for (int i = 0; i < n; i++)
            {
                int prev = 1;
                for (int j = 0; j < i; j++)
                {                    
                    int temp = arr[j];

                    arr[j] = (j == 0 ? 0 : prev) + arr[j];
                    prev = temp;
                }

                arr[i] = prev;

                for (int p = 0; p <= i; p++)
                {
                    Console.Write("{0} ", arr[p]);
                }

                Console.WriteLine();
            }
        }
    }
}
