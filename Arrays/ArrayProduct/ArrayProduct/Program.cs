using System;
namespace Solution
{
    class Solution
    {
        static void Main(string[] args)
        {
            int[] arr = { 2, 3, 5, 4, 6 };
            long[] result = ArrayProduct(arr);

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
        }

        // Taking input of int and returning long to reduce
        // chances of overflow (it can still happen, but
        // at least we considered the option)
        //
        static long[] ArrayProduct(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                throw new ArgumentException("Invalid input");
            }

            int len = arr.Length;
            long[] products = new long[len];

            // populate products from reverse
            //
            products[len - 1] = arr[len - 1];
            for (int i = len - 2; i >= 0; i--)
            {
                // Keep checked so it would throw in case of overflows
                //
                checked
                {
                    products[i] = products[i + 1] * (long)arr[i];
                }
            }

            // Now calculate products forward
            //
            long currProduct = 1;
            for (int i = 0; i < len - 1; i++)
            {
                products[i] = currProduct * products[i + 1];
                currProduct = currProduct * arr[i];
            }

            products[len - 1] = currProduct;
            return products;
        }
    }
}