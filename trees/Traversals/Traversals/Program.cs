using System;
using System.Collections.Generic;
using System.Linq;

namespace Traversals
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxVal = 999;
            const int elems = 99999;

            int[] arr = new int[elems];
            Random r = new Random();

            for (int i = 0; i < elems; i++)
            {
                arr[i] = r.Next(maxVal);
            }

            BinarySearchTree bst = BinarySearchTree.MakeFrom(arr);

            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            Action<int> a = (num) => l1.Add(num);
            Action<int> b = (num) => l2.Add(num);

            Traversals.PostOrderRecursive(bst.root, a);
            Traversals.PostOrderNonRecursive(bst.root, b);

            if (l1.Count != l2.Count)
            {
                throw new InvalidOperationException("Coding bug");
            }

            for (int i = 0; i < elems; i++)
            {
                if (l1[i] != l2[i]) throw new InvalidOperationException("Coding bug");
            }
        }
    }

    static class Traversals
    {
        public static void InOrderRecursive(TreeNode root, Action<int> a)
        {
            if (root != null)
            {
                InOrderRecursive(root.left, a);
                a(root.data);
                InOrderRecursive(root.right, a);
            }
        }

        public static void PreOrderRecursive(TreeNode root, Action<int> a)
        {
            if (root != null)
            {
                a(root.data);
                PreOrderRecursive(root.left, a);
                PreOrderRecursive(root.right, a);
            }
        }

        public static void PostOrderRecursive(TreeNode root, Action<int> a)
        {
            if (root != null)
            {
                PostOrderRecursive(root.left, a);
                PostOrderRecursive(root.right, a);
                a(root.data);
            }
        }

        public static void InOrderNonRecursive(TreeNode root, Action<int> a)
        {
            if (root == null) return;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                TreeNode curr = stack.Peek();
                curr = curr.left;

                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }

                curr = stack.Pop();
                a(curr.data);

                while (stack.Any() && curr.right == null)
                {
                    curr = stack.Pop();
                    a(curr.data);
                }

                if (curr.right != null)
                {
                    stack.Push(curr.right);
                }
            }
        }

        public static void PreOrderNonRecursive(TreeNode root, Action<int> a)
        {
            if (root == null) return;
            
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                var node = stack.Pop();
                a(node.data);

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            }
        }

        public static void PostOrderNonRecursive(TreeNode root, Action<int> a)
        {
            if (root == null) return;

            TreeNode prev = null;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                TreeNode curr = stack.Peek();

                if ((curr.left == null && curr.right == null) ||
                    (curr.right != null && object.ReferenceEquals(prev, curr.right)) ||
                    (curr.right == null && curr.left != null && object.ReferenceEquals(prev, curr.left)))
                {
                    prev = stack.Pop();
                    a(prev.data);
                    continue;
                }

                if (curr.right != null)
                {
                    stack.Push(curr.right);
                }
                if (curr.left != null)
                {
                    stack.Push(curr.left);
                }
            }
        }
    }

    class BinarySearchTree
    {
        public TreeNode root;

        public static BinarySearchTree MakeFrom(int[] array)
        {
            TreeNode r = null;
            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    r = AddToBST(r, array[i]);
                }
            }

            return new BinarySearchTree(r);
        }

        private static TreeNode AddToBST(TreeNode r, int item)
        {
            if (r == null)
            {
                return GetNode(item);
            }

            if (item <= r.data)
            {
                r.left = AddToBST(r.left, item);
            }
            else
            {
                r.right = AddToBST(r.right, item);
            }

            return r;
        }

        private static TreeNode GetNode(int item)
        {
            return new TreeNode() { data = item, left = null, right = null };
        }

        private BinarySearchTree() : this(null)
        {
        }

        private BinarySearchTree(TreeNode other)
        {
            root = other;
        }
    }

    class TreeNode
    {
        public int data;
        public TreeNode left;
        public TreeNode right;
    }
}
