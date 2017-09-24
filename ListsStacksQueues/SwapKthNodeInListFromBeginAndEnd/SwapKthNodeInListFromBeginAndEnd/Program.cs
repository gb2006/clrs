using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapKthNodeInListFromBeginAndEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedListNode head = MakeList(100);

            PrintList(head);

            head = SwapKFromBeginAndEnd(head, 5);

            PrintList(head);
        }

        static LinkedListNode SwapKFromBeginAndEnd(
            LinkedListNode head,
            int k
            )
        {
            if (head == null || head.next == null) return head;

            // Using dummy  here to simplify handling of cases
            // where head node is being swapped
            //
            LinkedListNode dummy = new LinkedListNode();
            dummy.next = head;

            // Using separate variable p here (instead of head) because
            // we want p to start from dummy to maintain consistency
            // below when p and node1 traverse together. If we use head
            // head will be ahead of node1 in traversal
            //
            LinkedListNode p = dummy;
            LinkedListNode prev1 = null;
            LinkedListNode prev2 = null;
            LinkedListNode node1 = dummy;
            LinkedListNode node2 = dummy;

            while (p != null && k > 0)
            {
                prev1 = node1;
                node1 = node1.next;
                p = p.next;
                k--;
            }

            if (p == null) throw new ArgumentException("Invalid k value");

            while (p != null)
            {
                prev2 = node2;
                node2 = node2.next;
                p = p.next;
            }

            // At this point, prev1 points to k-1 location node and node1 to kth node
            // Similarly, prev2 points to (n-k)-1 location node and node2 to n-k node
            //
            prev1.next = node2;
            prev2.next = node1;

            LinkedListNode temp = node1.next;
            node1.next = node2.next;
            node2.next = temp;

            return dummy.next;
        }

        static LinkedListNode MakeList(int num)
        {
            Random r = new Random();
            LinkedListNode head = null;

            while (num > 0)
            {
                LinkedListNode temp = new LinkedListNode { val = r.Next(1000), next = null };
                temp.next = head;
                head = temp;
                num--;
            }

            return head;
        }

        static void PrintList(LinkedListNode head)
        {
            while (head != null)
            {
                Console.Write(head.val + "->");
                head = head.next;
            }
            Console.WriteLine("null");
        }
    }

    public class LinkedListNode
    {
        public int val;
        public LinkedListNode next;
    }
}
