using System;

namespace NutsAndBolts
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] nuts =  { "N7", "N8", "N9", "N2", "N15", "N253", "N3", "N4", "N1", "N100", "N97", "N56"};
            string[] bolts = { "B1", "B3", "B9", "B8", "B4", "B7", "B2", "B97", "B100", "B56", "B15", "B253"};

            QuickSort(nuts, bolts, 0, nuts.Length - 1);
            for (int i = 0; i < nuts.Length; i++)
            {
                Console.WriteLine("{0} {1}", nuts[i], bolts[i]);
            }
        }

        // start and end are both inclusive indices
        //
        static void QuickSort(string[] nuts, string[] bolts, int st, int en)
        {
            if (st < en)
            {
                int pivotIndex1 = Partition(bolts, st, en, nuts[st]);
                int pivotIndex2 = Partition(nuts, st, en, bolts[pivotIndex1]);

                // this is more of an assert, these should always match
                //
                if (pivotIndex1 != pivotIndex2)
                {
                    throw new InvalidOperationException("Unexpected both pivot indices to differ");
                }

                QuickSort(nuts, bolts, st, pivotIndex1 - 1);
                QuickSort(nuts, bolts, pivotIndex1 + 1, en);
            }
        }

        // start and end are both inclusive indices.
        // Returns the index at which pivot is placed.
        //
        static int Partition(string[] arr, int st, int en, string pivot)
        {
            int pivotIndex = -1;
            int i = st - 1;

            for (; st <= en; st++)
            {
                int cmp = Compare(arr[st], pivot);
                if (cmp < 1)
                {
                    i++;
                    Swap(arr, i, st);

                    if (cmp == 0)
                    {
                        pivotIndex = i;
                    }
                }
            }

            if (pivotIndex == -1)
            {
                throw new InvalidOperationException("Invalid input");
            }

            Swap(arr, i, pivotIndex);

            // i is the location at which pivot is placed.
            return i;
        }

        // Assumes that indices are always valid.
        //
        static void Swap(string[] arr, int idx1, int idx2)
        {
            string temp = arr[idx1];
            arr[idx1] = arr[idx2];
            arr[idx2] = temp;
        }

        // Assumes that strings are of the form:
        // N<number> or B<number> 
        // such as N3, B10 or N11, B2 etc.
        //
        static int Compare(string s1, string s2)
        {
            int n1 = int.Parse(s1.Substring(1));
            int n2 = int.Parse(s2.Substring(1));
            return n1.CompareTo(n2);
        }
    }
}
