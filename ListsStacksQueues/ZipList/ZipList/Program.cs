using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipList
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode head = MakeList(97);
            PrintList(head);

            Console.WriteLine();

            head = ZipList(head);
            PrintList(head);
        }

        static ListNode ZipList(ListNode head)
        {
            if (head == null) return head;

            ListNode saved = head;
            ListNode middle = FindMiddleNode(head);
            ListNode reversedFromMiddle = ReverseList(middle.next);

            middle.next = null;

            // Merge two lists
            while (head != null && reversedFromMiddle != null)
            {
                ListNode temp1 = head.next;
                head.next = reversedFromMiddle;

                ListNode temp2 = reversedFromMiddle.next;
                reversedFromMiddle.next = temp1;

                head = temp1;
                reversedFromMiddle = temp2;                
            }

            return saved;
        }

        static ListNode ReverseList(ListNode head)
        {
            if (head == null) return head;

            ListNode prev = null;
            while (head != null)
            {
                ListNode temp = head.next;
                head.next = prev;
                prev = head;
                head = temp;
            }

            return prev;
        }

        static ListNode FindMiddleNode(ListNode head)
        {
            if (head == null) return head;

            ListNode slow = head;
            ListNode fast = head.next;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }

        static ListNode MakeList(int num)
        {
            ListNode head = null;
            Random r = new Random();

            while (num > 0)
            {
                ListNode temp = new ListNode(r.Next(1000));
                temp.next = head;
                head = temp;
                num--;
            }

            return head;
        }

        static void PrintList(ListNode head)
        {
            while (head != null)
            {
                Console.Write(head.val + "->");
                head = head.next;
            }
            Console.WriteLine("null");
        }
    }

    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int v)
        {
            val = v;
            next = null;
        }
    }
}
