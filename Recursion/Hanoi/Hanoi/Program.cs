using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            const int discs = 5;
            HanoiStack src, dest, temp;

            src = new HanoiStack("src");
            dest = new HanoiStack("dest");
            temp = new HanoiStack("temp");

            for (int i = discs; i > 0; i--)
            {
                src.Push(i);
            }

            Console.WriteLine("Before transfer:");
            Console.WriteLine(src);
            Console.WriteLine(dest);
            Console.WriteLine(temp);

            TransferHanoiStack(src, dest, temp, discs);

            Console.WriteLine("After transfer:");
            Console.WriteLine(src);
            Console.WriteLine(dest);
            Console.WriteLine(temp);
        }

        static void TransferHanoiStack(HanoiStack src, HanoiStack dest, HanoiStack temp, int srcSize)
        {
            if (srcSize == 1)
            {
                int disc = src.Pop();

                Console.WriteLine("Moving {0} from {1} to {2}", disc, src.Name, dest.Name);

                dest.Push(disc);
                return;
            }

            TransferHanoiStack(src, temp, dest, srcSize - 1);
            TransferHanoiStack(src, dest, temp, 1);
            TransferHanoiStack(temp, dest, src, srcSize - 1);
        }
    }

    class HanoiStack
    {
        private Stack<int> stack;
        private string name;

        public HanoiStack(string n)
        {
            stack = new Stack<int>();
            name = n;
        }

        public string Name
        {
            get { return name; }
        }

        public void Push(int discSize)
        {
            if (!stack.Any() || (stack.Peek() > discSize))
            {
                stack.Push(discSize);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format("Cannot push disc with size {0} on top of {1}",
                    discSize,
                    stack.Peek()));
            }
        }

        public int Pop()
        {
            if (stack.Any())
            {
                return stack.Pop();
            }
            throw new InvalidOperationException("Popping an empty stack");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name).Append(": ");
            ToString(sb);
            return sb.ToString();
        }

        private void ToString(StringBuilder sb)
        {
            if (stack.Any())
            {
                int i = stack.Pop();
                sb.Append(i).Append(" ");
                ToString(sb);
                stack.Push(i);
            }
        }
    }
}
