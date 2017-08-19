using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 3;

            MinHeap mh = new MinHeap(100);
            Random r = new Random();

            for (int i = 0; i < size; i++)
            {
                mh.Insert(r.Next(1000));
            }

            while (!mh.IsEmpty())
            {
                Console.Write(mh.ExtractMin() + " ");
            }

            Console.WriteLine();
        }
    }

    class MinHeap
    {
        private int[] heap;
        private int count;

        public MinHeap(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("size");
            }

            heap = new int[size];
            count = 0;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public bool IsFull()
        {
            return count == heap.Length;
        }

        public int Min()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }
            return heap[0];
        }

        public void Insert(int e)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("Heap is full");
            }

            heap[count++] = e;

            for (int idx = count - 1; idx > 0; )
            {
                int iop = IndexOfParent(idx);

                if (heap[idx] < heap[iop])
                {
                    Swap(idx, iop);
                    idx = iop;
                }
                else
                {
                    break;
                }
            }
        }

        public int ExtractMin()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }

            int min = heap[0];
            heap[0] = heap[count - 1];
            count--;

            if (count > 0)
            {
                MinHeapify(0);
            }
            
            return min;
        }

        public void UpdateMin(int newValue)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }

            heap[0] = newValue;
            MinHeapify(0);
        }

        private void MinHeapify(int idx)
        {
            int leftIdx = IndexOfLeft(idx);
            int rightIdx = IndexOfRight(idx);
            int minIdx = idx;

            if (leftIdx < count && heap[leftIdx] < heap[minIdx])
            {
                minIdx = leftIdx;
            }

            if (rightIdx < count && heap[rightIdx] < heap[minIdx])
            {
                minIdx = rightIdx;
            }

            if (minIdx != idx)
            {
                Swap(minIdx, idx);
                MinHeapify(minIdx);
            }
        }

        private int IndexOfLeft(int idx)
        {
            return 2 * idx + 1;
        }

        private int IndexOfRight(int idx)
        {
            return (2 * idx) + 2;
        }

        private int IndexOfParent(int idx)
        {
            return (idx == 0) ? -1 : (idx - 1) / 2;
        }

        // Assumes indices are valid
        //
        private void Swap(int idx1, int idx2)
        {
            int temp = heap[idx1];
            heap[idx1] = heap[idx2];
            heap[idx2] = temp;
        }
    }
}
