using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestKPointsHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<int, int>[] points = new Tuple<int, int>[]
            {
                new Tuple<int, int>(15, 66),
                new Tuple<int, int>(21, 33),
                new Tuple<int, int>(12, 22),
                new Tuple<int, int>(5, 8),
                new Tuple<int, int>(8, 8),
                new Tuple<int, int>(10, 20),
                new Tuple<int, int>(22, 10),
                new Tuple<int, int>(5, 6),
                new Tuple<int, int>(4, 6)
                
            };

            Tuple<int, int> testPoint = new Tuple<int, int>(4, 6);
            int k = 3;

            var result = FindNearestKPoints(points, testPoint, k);
            foreach (var item in result)
            {
                Console.WriteLine("({0},{1})", item.Item1, item.Item2);
            }
        }
        
        static double Distance(Tuple<int, int> p1, Tuple<int, int> p2)
        {
            int x2 = (p1.Item1 - p2.Item1);
            x2 = x2 * x2;

            int y2 = (p1.Item2 - p2.Item2);
            y2 = y2 * y2;

            return Math.Sqrt(x2 + y2);
        }

        static Tuple<int, int>[] FindNearestKPoints(Tuple<int, int>[] points, Tuple<int, int> testPoint, int k)
        {
            if (k >= points.Length)
            {
                return points;
            }

            Tuple<int, int>[] retArr = new Tuple<int, int>[k];
            PriorityQueue pq = new PriorityQueue(k);

            for (int i = 0; i < points.Length; i++)
            {
                double dist = Distance(points[i], testPoint);

                if (pq.IsFull())
                {
                    // two points can have same distance to test point
                    // which means we should use <= in the comparison
                    // below.
                    //
                    if (dist <= pq.MaxKey())
                    {
                        pq.UpdateMaxKeyVal(dist, i);
                    }
                }
                else
                {
                    pq.Insert(dist, i);
                }
            }

            for (int i = 0; !pq.IsEmpty(); i++)
            {
                retArr[i] = points[pq.ExtractMaxVal()];
            }

            return retArr;
        }
    }

    class PriorityQueue
    {
        class PQObject
        {
            public double Key;
            public int Val;
        }

        private PQObject[] heap;
        private int count;

        public PriorityQueue(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("size");
            }

            heap = new PQObject[size];
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

        public void Insert(double key, int val)
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

                if (heap[idx].Key > heap[iop].Key)
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

        public double MaxKey()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }
            return heap[0].Key;
        }

        public int ExtractMaxVal()
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
                MaxHeapify(0);
            }

            return val;
        }

        public void UpdateMaxKeyVal(double key, int val)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Heap is empty");
            }

            heap[0].Key = key;
            heap[0].Val = val;

            MaxHeapify(0);
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
                MaxHeapify(maxIdx);
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
