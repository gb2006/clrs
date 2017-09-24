using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseKGroup
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode head = MakeList(100);
            PrintList(head);

            Console.WriteLine();

            head = ReverseKGroup(head, 1);
            PrintList(head);
        }

        public static ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode dummy = new ListNode(0);
            dummy.next = head;

            ListNode current = dummy;

            while (current.next != null)
            {
                ListNode save = current.next;
                var ret = ReverseK(current.next, k);

                current.next = ret.Item1;
                save.next = ret.Item2;
                current = save;
            }

            return dummy.next;
        }

        private static Tuple<ListNode, ListNode> ReverseK(ListNode node, int k)
        {
            ListNode prev = null;

            while (node != null && k > 0)
            {
                ListNode temp = node.next;
                node.next = prev;
                prev = node;
                node = temp;
                k--;
            }

            return new Tuple<ListNode, ListNode>(prev, node);
        }

        private static ListNode MakeList(int num)
        {
            Random r = new Random();
            ListNode head = null;

            while (num > 0)
            {
                ListNode temp = new ListNode(r.Next(1000));
                temp.next = head;
                head = temp;
                num--;
            }

            return head;
        }

        private static void PrintList(ListNode head)
        {
            while (head != null)
            {
                Console.Write(head.val + "->");
                head = head.next;
            }
            Console.WriteLine("null");
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
}
