namespace EvenOddSplit
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static int[] groupNumbers(int[] arr)
        {
            if (arr == null || arr.Length <= 1)
            {
                return arr;
            }

            int marker = 0;
            for (int k = 0; k < arr.Length; k++)
            {
                if (arr[k] % 2 == 0)
                {
                    int temp = arr[marker];
                    arr[marker] = arr[k];
                    arr[k] = temp;
                    marker++;
                }
            }
            return arr;
        }
    }
}
