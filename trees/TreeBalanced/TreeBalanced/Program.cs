using System;

namespace TreeBalanced
{
    // check if a binary tree is balanced. A balanced tree is defined
    // to be a tree such that the heights of the two subtrees of any
    // node never differ by more than one.
    class Program
    {
        class Node
        {
            public int val;
            public Node left;
            public Node right;
        }

        static bool IsBalanced(Node root, ref int height)
        {
            if (root == null)
            {
                height = 0;
                return true;
            }

            int leftHeight = 0;
            int rightHeight = 0;

            if (IsBalanced(root.left, ref leftHeight) &&
                IsBalanced(root.right, ref rightHeight) &&
                Math.Abs(leftHeight - rightHeight) <= 1)
            {
                height = Math.Max(leftHeight, rightHeight) + 1;
                return true;
            }

            return false;
        }
    }
}
