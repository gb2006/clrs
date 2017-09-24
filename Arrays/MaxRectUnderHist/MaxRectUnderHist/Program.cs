using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxRectUnderHist
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static int MaxRectangleUnderHistogram(int[] array)
        {
            if (array == null || array.Length < 2)
            {
                throw new ArgumentException("array");
            }

            Stack<int> s = new Stack<int>();
            int i = 0;
            int maxArea = 0;
            int tempArea = 0;

            while (i < array.Length)
            {
                if (!s.Any() || array[s.Peek()] <= array[i])
                {
                    s.Push(i++);
                }
                else
                {
                    int height = array[s.Peek()];
                    s.Pop();
                    int width = s.Any() ? i - s.Peek() - 1 : 0;

                    tempArea = width * height;
                    if (tempArea > maxArea)
                    {
                        maxArea = tempArea;
                    }
                }
            }

            while (s.Any())
            {
                int height = array[s.Peek()];
                s.Pop();
                int width = s.Any() ? i - s.Peek() - 1 : 0;

                tempArea = width * height;
                if (tempArea > maxArea)
                {
                    maxArea = tempArea;
                }
            }

            return maxArea;
        }
    }
}
