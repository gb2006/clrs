using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssortedAlgos
{
    class MergeSort
    {
        public void Sort(int[] arr)
        {
            if (arr == null || arr.Length <= 1) { return; }
            Sort(arr, 0, arr.Length - 1);            
        }

        // both start and end are inclusive
        //
        private void Sort(int[] arr, int st, int en)
        {
            if (st < en)
            {
                int mid = (en - st) / 2;
                mid = st + mid;

                Sort(arr, st, mid);
                Sort(arr, mid + 1, en);
                Merge(arr, st, mid, en);
            }
        }

        private void Merge(int[] arr, int st, int mid, int en)
        {
            int[] copyArr = new int[en - st + 1];
            int i = st;
            int j = mid + 1;
            int k = 0;

            while (i <= mid && j <= en)
            {
                if (arr[i] <= arr[j])
                {
                    copyArr[k++] = arr[i++];
                }
                else
                {
                    copyArr[k++] = arr[j++];
                }
            }

            while (i <= mid)
            {
                copyArr[k++] = arr[i++];
            }

            while (j <= en)
            {
                copyArr[k++] = arr[j++];
            }

            copyArr.CopyTo(arr, st);
        }
    }
}
