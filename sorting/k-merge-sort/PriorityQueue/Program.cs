using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] arr = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 0, 2 }
            };

            int[] result = mergearrays(arr);
            foreach (var i in result)
            {
                Console.WriteLine(i);
            }
        }
     
        static int[] mergearrays(int[][] iarray)
        {
            // Ignoring input check for now.
            //

            // We assume that even though this is jagged array that
            // each subarray is the same length.
            //
            int[] retArr = new int[iarray.Length * iarray[0].Length];
            int[] idxOfEachArr = new int[iarray.Length];
            int k = 0;

            // Create a priority queue of K elements
            //
            PriorityQueue pq = new PriorityQueue(iarray.Length, iarray[0][0] < iarray[0][1]);

            // Initially all indices point to 0th entry of correponding array
            //
            for (int i = 0; i < idxOfEachArr.Length; i++)
            {
                idxOfEachArr[i] = 0;
            }

            // Seed the priority queue with 0th element of each array.
            //
            for (int i = 0; i < iarray.Length; i++)
            {
                pq.Insert(iarray[i][0], i);
            }

            while (!pq.IsEmpty())
            {
                int idx = pq.ExtractFirst();

                retArr[k++] = iarray[idx][idxOfEachArr[idx]];
                idxOfEachArr[idx]++;

                if (idxOfEachArr[idx] < iarray[idx].Length)
                {
                    pq.Insert(iarray[idx][idxOfEachArr[idx]], idx);
                }
            }

            return retArr;
        }
    }

    class PriorityQueue
    {
        class PQObject
        {
            public int Key;
            public int Val;
        }

        private PQObject[] heap;
        private int count;
        private bool isMinHeap;

        public PriorityQueue(int size, bool isAscendingOrder)
        {
            if (size <= 0)
            {
                throw new ArgumentException("size");
            }

            heap = new PQObject[size];
            isMinHeap = isAscendingOrder;
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

        public void Insert(int key, int val)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("Heap is full");
            }

            PQObject pqObj = new PQObject { Key = key, Val = val };
            heap[count++] = pqObj;

            for (int idx = count - 1; idx > 0;)
            {
                int iop = IndexOfParent(idx);

                if ( (isMinHeap && (heap[idx].Key < heap[iop].Key)) ||
                     (!isMinHeap && (heap[idx].Key > heap[iop].Key)) )
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

        public int ExtractFirst()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }

            int val = heap[0].Val;
            heap[0] = heap[count - 1];
            count--;

            if (count > 0)
            {
                if (isMinHeap) { MinHeapify(0); }
                else { MaxHeapify(0); }
            }

            return val;
        }

        private void MaxHeapify(int idx)
        {
            int leftIdx = IndexOfLeft(idx);
            int rightIdx = IndexOfRight(idx);
            int maxIdx = idx;

            if (leftIdx < count && heap[leftIdx].Key > heap[maxIdx].Key)
            {
                maxIdx = leftIdx;
            }

            if (rightIdx < count && heap[rightIdx].Key > heap[maxIdx].Key)
            {
                maxIdx = rightIdx;
            }

            if (maxIdx != idx)
            {
                Swap(maxIdx, idx);
                MinHeapify(maxIdx);
            }
        }

        private void MinHeapify(int idx)
        {
            int leftIdx = IndexOfLeft(idx);
            int rightIdx = IndexOfRight(idx);
            int minIdx = idx;

            if (leftIdx < count && heap[leftIdx].Key < heap[minIdx].Key)
            {
                minIdx = leftIdx;
            }

            if (rightIdx < count && heap[rightIdx].Key < heap[minIdx].Key)
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
            PQObject temp = heap[idx1];
            heap[idx1] = heap[idx2];
            heap[idx2] = temp;
        }
    }
}
