namespace AssortedAlgos
{
    class InsertionSort
    {
        public void Sort(int[] arr)
        {
            if (arr == null || arr.Length <= 1) { return; }

            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }
        }
    }
}
