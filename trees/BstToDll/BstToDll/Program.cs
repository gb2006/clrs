using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BstToDll
{
    class Program
    {
        class TreeNode
        {
            int val;
            TreeNode left;
            TreeNode right;
        }
        static void BstToDLL(TreeNode node)
        {
            if (node == null) return;

            TreeNode head = null;
            TreeNode prev = null;

            BstToDll(node, ref head, ref prev);

            // Assert that head and prev are not null here
            // They cannot be null because the only way for that
            // to happen is if node was null, but we already 
            // took care of that at the entry of this function.
            //
            head.left = prev;
            prev.right = head;

            while (!object.ReferenceEquals(head, prev))
            {
                Console.Write("{0} ", head.val);
                head = head.right;
            }

            Console.WriteLine("{0}", head.val);
        }

        static void BstToDll(TreeNode node, ref TreeNode head, ref TreeNode prev)
        {
            if (node == null) return;

            BstToDll(node.left, ref head, ref prev);
            if (head == null)
            {
                head = node;
            }
            if (prev != null)
            {
                prev.right = node;
                node.left = prev;
            }
            prev = node;
            BstToDll(node.right, ref head, ref prev);
        }
    }
}
