using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTFromSortedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[1000];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 2;
            }

            Node root = BuildBST(array);
            InOrder(root);
        }

        static void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.left);
                Console.Write("{0} ", root.val);
                InOrder(root.right);
            }
        }

        // Given a sorted array, build a minimum height BST from it.
        class Node
        {
            public int val;
            public Node left;
            public Node right;
        }

        static Node BuildBST(int[] array)
        {
            if (array == null || array.Length == 0) { return null; }
            return BuildBST(array, 0, array.Length - 1);
        }

        static Node BuildBST(int[] array, int st, int en)
        {
            if (st > en)
            {
                return null;
            }

            int mid = st + ((en - st) / 2);

            Node root = new Node();
            root.val = array[mid];
            root.left = BuildBST(array, st, mid - 1);
            root.right = BuildBST(array, mid + 1, en);

            return root;
        }
    }
}
