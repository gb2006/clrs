namespace AssortedAlgos
{
    class SelectionSort
    {
        public void Sort(int[] arr)
        {
            if (arr == null || arr.Length <= 1) { return; }

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }
    }
}
