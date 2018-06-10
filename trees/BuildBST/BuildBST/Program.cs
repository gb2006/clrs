using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBST
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            const int items = 2;
            const int maxVal = 999;

            int[] array1 = new int[items];
            int[] array2 = new int[items];

            for (int i = 0; i < items; i++)
            {
                array1[i] = r.Next(maxVal);
                array2[i] = r.Next(maxVal);
            }

            BinarySearchTree bst1 = BinarySearchTree.MakeFrom(array1);
            BinarySearchTree bst2 = BinarySearchTree.MakeFrom(array2);

            bst1.CombineWith(bst2);
            bst1.InorderTraverse(i => Console.Write(i + " "));
            Console.WriteLine();
        }

        static bool IsSorted(IEnumerable<int> items)
        {
            var enumerator = items.GetEnumerator();
            if (enumerator.MoveNext())
            {
                int curr = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    if (curr > enumerator.Current)
                    {
                        return false;
                    }
                    curr = enumerator.Current;
                }
            }

            return true;
        }
    }

    class BinarySearchTree
    {
        private TreeNode root;

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

        public void InorderTraverse(Action<int> a)
        {
            InorderTraverse(root, a);
        }

        private void InorderTraverse(TreeNode r, Action<int> a)
        {
            if (r != null)
            {
                InorderTraverse(r.left, a);
                a(r.data);
                InorderTraverse(r.right, a);
            }
        }

        public bool IsValidBST2()
        {
            int? prev = null;
            return IsValidBST2(root, ref prev);
        }

        // ref is needed. It won't work without ref type
        //
        private bool IsValidBST2(TreeNode node, ref int? prev)
        {
            if (node == null)
            {
                return true;
            }

            if (!IsValidBST2(node.left,ref prev))
            {
                return false;
            }

            if (prev.HasValue && prev.Value >= node.data)
            {
                return false;
            }

            prev = node.data;
            return IsValidBST2(node.right, ref prev);
        }

        public bool IsValidBST()
        {
            return IsValidBST(root, null, null);
        }

        private bool IsValidBST(TreeNode n, TreeNode min, TreeNode max)
        {
            if (n == null)
            {
                return true;
            }

            if ( (min != null && n.data < min.data) ||
                 (max != null && n.data > max.data) )
            {
                return false;
            }

            return IsValidBST(n.left, min, n) && 
                   IsValidBST(n.right, n, max);
        }

        public int MinFromTree()
        {
            if (root == null) throw new InvalidOperationException("Empty tree");

            return MinFromTree(root).Value;
        }

        private int? MinFromTree(TreeNode n)
        {
            if (n == null)
            {
                return null;
            }

            return Min(n.data, MinFromTree(n.left), MinFromTree(n.right));
        }

        private int Min(int val1, int? val2, int? val3)
        {
            int min = val1;
            if (val2.HasValue && val2.Value < min)
            {
                min = val2.Value;
            }
            if (val3.HasValue && val3.Value < min)
            {
                min = val3.Value;
            }
            return min;
        }

        public void CombineWith(BinarySearchTree bst)
        {
            TreeNode head1 = this.ToLinkedList();
            TreeNode head2 = bst != null ? bst.ToLinkedList() : null;

            int length;
            TreeNode mergedList = this.MergeLists(head1, head2, out length);
            root = MakeBST(mergedList, length);
        }

        private TreeNode MergeLists(TreeNode l1, TreeNode l2, out int length)
        {
            TreeNode dummy = GetNode(0);
            TreeNode curr = dummy;
            int count = 0;

            while (l1 != null && l2 != null)
            {
                if (l1.data < l2.data)
                {
                    curr.right = l1;
                    l1 = l1.right;
                }
                else
                {
                    curr.right = l2;
                    l2 = l2.right;
                }
                curr = curr.right;
                count++;
            }

            while (l1 != null)
            {
                curr.right = l1;
                l1 = l1.right;
                curr = curr.right;
                count++;
            }

            while (l2 != null)
            {
                curr.right = l2;
                l2 = l2.right;
                curr = curr.right;
                count++;
            }

            length = count;
            return dummy.right;
        }

        private TreeNode MakeBST(TreeNode sortedList, int len)
        {
            if (len <= 1)
            {
                return sortedList;
            }

            TreeNode head = sortedList;
            TreeNode tail = head;
            TreeNode prev = head;

            for (int i = 0; i < len / 2; i++)
            {
                prev = tail;
                tail = tail.right;
            }

            prev.right = null;

            TreeNode innerRoot = tail;
            tail = tail.right;

            innerRoot.left = MakeBST(head, len / 2);
            innerRoot.right = MakeBST(tail, len - 1 - (len / 2));

            return innerRoot;
        }

        private TreeNode ToLinkedList()
        {
            TreeNode head = null;
            TreeNode prev = null;

            ToLinkedList(root, ref head, ref prev);
            return head;
        }

        private void ToLinkedList(TreeNode n, ref TreeNode head, ref TreeNode prev)
        {
            if (n != null)
            {
                ToLinkedList(n.left, ref head, ref prev);
                n.left = null;
                if (head == null)
                {
                    head = n;
                }
                if (prev != null)
                {
                    prev.right = n;
                }
                prev = n;
                ToLinkedList(n.right, ref head, ref prev);
            }
        }

        private void Traverse(TreeNode n)
        {
            while (n != null)
            {
                Console.Write("{0} ", n.data);
                n = n.right;
            }
            Console.WriteLine();
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
