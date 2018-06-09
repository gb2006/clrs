using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicIndex
{
    class Program
    {
        // A magic index in an array A[0..n-1] is defined to be an
        // index such that A[i] = i. Given a sorted array of distinct
        // integers, write a method to find a magic index, if one
        // exists in array A.
        static void Main(string[] args)
        {
        }

        int FindMagicIndex(int[] arr, int lo, int hi)
        {
            if (lo > hi)
            {
                return -1;
            }

            int mid = lo + ((hi - lo) / 2);
            if (arr[mid] > mid)
            {
                // check in lower half
                return FindMagicIndex(arr, lo, mid - 1);
            }
            else if (arr[mid] < mid)
            {
                // check in upper half
                return FindMagicIndex(arr, mid + 1, hi);
            }
            else if (arr[mid] == mid)
            {
                return mid;
            }

            throw new Exception("Shouldn't get here");
        }

        // Distinct sorted elements
        // Two cases to consider
        // Array contains only >= 0 numbers.
        // Or, array contains both -ve & +ve numbers along with 0.
        // Key insight: array index is monotonically increasing
        // In sorted distinct case, values are also monotonically increasing
        // 
        // With binary search, let's find middle element -- if that's == index, we're done.
        // otherwise if element at middle index is < index --> Only possible to find in upper half
        // otherwise if element at middle index is > index --> Only possible to find in lower half
    }
}
