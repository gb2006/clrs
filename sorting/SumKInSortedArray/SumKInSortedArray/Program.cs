using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumKInSortedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] array = { 4, 3, 2, 5, 5, 7, 9, 11 };
            // int[] array = { 4, 7 };
            // int[] array = { -3, -1, -3, -2, -4 };
            int[] array = { 1, 3, 5, 2, 4, 38, 11, 9, 22, 10, 12, 7, 6, 5, 11, 1, 0, 10, 14 };

            if (array == null || array.Length < 3)
            {
                return;
            }

            Array.Sort(array);
            List<string> list = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0) break;
                GetSumKPairs(array, -array[i], i + 1, list);
            }

            // consider this input: 3 0 0 0
        }

        // Assumes that input array is already sorted
        //
        static void GetSumKPairs(int[] array, int k, int startIndex, List<string> list)
        {
            int left = startIndex;
            int right = array.Length - 1;

            while (left < right)
            {
                int sum = array[left] + array[right];
                if (sum == k)
                {
                    int lVal = array[left];
                    int rVal = array[right];

                    // Skip over duplicates on both sides
                    //
                    while (right > left && array[right] == rVal)
                    {
                        right--;
                    }

                    while (left < right && array[left] == lVal)
                    {
                        left++;
                    }

                    list.Add(string.Format("{0},{1},{2}", -k, lVal, rVal));
                }
                else if (sum > k)
                {
                    right--;
                }
                else // sum < k
                {
                    left++;
                }
            }
        }
    }
}
