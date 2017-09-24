using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinStack
{
    class Program
    {
        // Implement a Min stack
        // It supports GetMinimum function which returns
        // minimum value seen so far on the stack. The
        // GetMinimum is constrained to return in const time
        //
        static void Main(string[] args)
        {
            MinStack ms = new MinStack();
            ms.Push(1);
            ms.Push(3);
            ms.Push(2);
            ms.Push(1);

            Console.WriteLine(ms.GetMinimum());
            ms.Pop();
            Console.WriteLine(ms.GetMinimum());
            ms.Pop();
            Console.WriteLine(ms.GetMinimum());

        }
    }

    internal class MinStack
    {
        private Stack<int> actual;
        private Stack<int> minSofar;

        internal MinStack()
        {
            actual = new Stack<int>();
            minSofar = new Stack<int>();
        }

        internal void Push(int n)
        {
            actual.Push(n);
            if (minSofar.Count == 0 || n <= minSofar.Peek())
            {
                minSofar.Push(n);
            }
        }

        internal int Pop()
        {
            if (actual.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            int n = actual.Pop();
            if (n == minSofar.Peek())
            {
                minSofar.Pop();
            }
            return n;
        }

        internal int GetMinimum()
        {
            if (actual.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return minSofar.Peek();
        }
    }
}
