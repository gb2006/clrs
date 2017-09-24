using System;

namespace AssortedAlgos
{
    class MergeSort2
    {
        public int[] Sort(int[] arr)
        {
            if (arr == null || arr.Length <= 1) { return arr; }

            return Sort(arr, 0, arr.Length - 1);
        }

        // both start and end are inclusive
        //
        private int[] Sort(int[] arr, int st, int en)
        {
            if (st < en)
            {
                int mid = (en - st) / 2;
                mid = st + mid;

                int[] left = Sort(arr, st, mid);
                int[] right = Sort(arr, mid + 1, en);
                return Merge(left, right);
            }
            else if (st == en)
            {
                return new int[] { arr[st] };
            }
            else
            {
                // This shouldn't happen.
                //
                throw new Exception("Unexpected condition");
            }
        }

        private int[] Merge(int[] left, int[] right)
        {
            int[] copyArr = new int[left.Length + right.Length];
            int i = 0;
            int j = 0;
            int k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    copyArr[k++] = left[i++];
                }
                else
                {
                    copyArr[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                copyArr[k++] = left[i++];
            }

            while (j < right.Length)
            {
                copyArr[k++] = right[j++];
            }

            return copyArr;
        }
    }
}
